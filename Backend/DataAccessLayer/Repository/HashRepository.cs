using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository
{
    public class HashRepository : RepositoryBase
    {
		public void addhash(List<string> hashes, int cid)
		{
			for (int i = 0; i < hashes.Count; i++)
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					String str = hashes[i];

					var hsh = dbContext.Hashes.Where(s => s.Content == str).FirstOrDefault();
					var maxid = 0;
					if (hsh == null)
					{
						Hash hash = new Hash()
						{
							Content = hashes[i],
							//PostDate = DateTime.Now,
							HashCount = 1,//Alert
							SearchCount = 0
						};
						dbContext.Hashes.Add(hash);
						dbContext.SaveChanges();
						//var maxid=dbContext.Hashes.SqlQuery<Int32>("Select Max(HashId) From Hash");
						maxid = dbContext.Hashes.Max(s => s.HashId);
						var x = dbContext.Hashes.Where(s => s.Content == hashes[i]);
						TweetHashMapper mp = new TweetHashMapper()
						{
							TweetId = cid,
							HashId = (int)maxid
						};
						dbContext.TweetHashMappers.Add(mp);

					}
					else
					{
						String st = hashes[i];
						Hash hash = dbContext.Hashes.Where(s => s.Content == st).FirstOrDefault();
						hash.HashCount += 1;
						TweetHashMapper mp = new TweetHashMapper()
						{
							TweetId = cid,
							HashId = hash.HashId
						};
						dbContext.TweetHashMappers.Add(mp);
						//dbContext.Database.SqlQuery<Hash>("Update hash set hashcount=hashcount+1 where Content=" + hashes[i]);

					}
					//dbContext.Tweets.Add(tweet);
					dbContext.SaveChanges();
				}


			}
			return;
		}
		public string GetMostTrendingHashtag()
        {
            string hashContent = null;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    hashContent = dbContext.Database.SqlQuery<string>("select Content from Hash h1 join (select top(1) MAX(HashCount+SearchCount) as maxcount, HashId from Hash group by HashId order by maxcount desc) as h2 on h1.hashid = h2.hashid").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hashContent;
        }
		public List<TweetDTO> GetAllSearchedTweets(string searchHash)
		{
			List<TweetDTO> retVal = null;
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{

					var hashBySearchId = (from h in dbContext.Hashes
										  where h.Content.ToLower().Contains(searchHash.ToLower())
										  select h.HashId).ToList();

					//var tweets = dbContext.Database.SqlQuery<TweetDTO>("select t.Content, t.PostDate, t.UserId, t.LikeCount, t.DislikeCount from TweetHashMapper m join Tweets t on m.TweetId=t.TweetId where m.HashId=" + hashBySearchId).ToList();

					var tweets = (from t in dbContext.Tweets
								  join mp in dbContext.TweetHashMappers on t.TweetId equals mp.TweetId
								  join h in dbContext.Hashes on mp.HashId equals h.HashId
								  where h.Content.ToLower().Contains(searchHash.ToLower()) && t.Active == true
								  select t).ToList();

					foreach (var hash in hashBySearchId)
					{
						var hashObj = dbContext.Hashes.Where(h => h.HashId == hash).FirstOrDefault();
						hashObj.SearchCount += 1;
					}
					dbContext.SaveChanges();

					retVal = this.Mapper.Map<List<TweetDTO>>(tweets);
					//foreach (var item in tweets)
					//{
					//    TweetDTO twt = new TweetDTO
					//    {
					//        Content = item.Content,
					//        PostDate = item.PostDate,
					//        UserId = item.UserId,
					//        LikeCount = (int)item.LikeCount,
					//        DislikeCount = (int)item.DislikeCount,
					//    };
					//    retVal.Add(twt);
					//}
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return retVal;
		}

		public List<SearchUserDTO> GetAllSearchUsers(string searchHash)
		{
			List<SearchUserDTO> retVal = new List<SearchUserDTO>();
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var users = dbContext.Users.Where(c => c.FirstName.Contains(searchHash) || c.LastName.Contains(searchHash) || c.EmailId.Contains(searchHash)).ToList();
					//retVal = users != null && users.Count > 0 ? new List<UserDTO>() : null;

					int curUserId = 1;
					SearchUserDTO searchHashPeopleDTO = null;
					foreach (var user in users)
					{
						var alreadyFollowing = dbContext.Follows.Where(f => f.FollowerId == curUserId && f.UserId == user.UserId && f.Active == true).FirstOrDefault();
						if (alreadyFollowing != null)
						{
							searchHashPeopleDTO = new SearchUserDTO
							{
								UserDTO = this.Mapper.Map<UserDTO>(user),
								AlreadyFollowing = true

							};

						}
						else
						{
							searchHashPeopleDTO = new SearchUserDTO
							{
								UserDTO = this.Mapper.Map<UserDTO>(user),
								AlreadyFollowing = false

							};

						}
						retVal.Add(searchHashPeopleDTO);
					}

					//retVal = this.Mapper.Map<List<UserDTO>>(users);

				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return retVal;
		}


		public void AddNewTweetHash(List<string> hashes, int tweetId)
        {
            for (int i = 0; i < hashes.Count; i++)
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    String str = hashes[i];

                    var hsh = dbContext.Hashes.Where(s => s.Content == str).FirstOrDefault();
                    var maxid = 0;
                    if (hsh == null)
                    {
                        Hash hash = new Hash()
                        {
                            Content = hashes[i],
                            HashCount = 1,//Alert
                            SearchCount = 0
                        };
                        dbContext.Hashes.Add(hash);
                        dbContext.SaveChanges();//
                        maxid = dbContext.Hashes.Max(s => s.HashId);
                        var x = dbContext.Hashes.Where(s => s.Content == hashes[i]);
                        TweetHashMapper mpEntry = new TweetHashMapper()
                        {
                            TweetId = tweetId,
                            HashId = (int)maxid 
                        };
                        dbContext.TweetHashMappers.Add(mpEntry);
                        dbContext.SaveChanges();//

                    }
                    else
                    {
                        String st = hashes[i];
                        Hash hash = dbContext.Hashes.Where(s => s.Content == st).FirstOrDefault();
                        hash.HashCount += 1;
                        TweetHashMapper mp = new TweetHashMapper()
                        {
                            TweetId = tweetId,
                            HashId = hash.HashId
                        };
                        dbContext.TweetHashMappers.Add(mp);

                    }
                   dbContext.SaveChanges();//
                }


            }
            return;
        }

        public bool updateHash(List<TweetHashMapper> tweetHashMappers, int tweetId)
        {
            //TweetDTO retVal = null;

            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    foreach (var x in tweetHashMappers)
                    {
                        var hash = dbContext.Hashes.Where(s => s.HashId == x.HashId).FirstOrDefault();
                        hash.HashCount -= 1;
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
		public bool updateHashforedit(List<TweetHashMapper> tweetHashMappers, int tweetId)
		{
			//TweetDTO retVal = null;

			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					foreach (var x in tweetHashMappers)
					{
						var hash = dbContext.Hashes.Where(s => s.HashId == x.HashId).FirstOrDefault();
						
						hash.HashCount -= 1;
						if (hash.HashCount <= 0)
						{
							dbContext.Hashes.Remove(hash);
						}
					}

					dbContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}
	}
}
