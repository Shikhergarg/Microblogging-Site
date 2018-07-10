using DataAccessLayer.Entity;
using System;
using AutoMapper;
using Shared.DataTransferObject;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TweetRepository : RepositoryBase
    {
		/// <summary>
		/// Getting tweet of the
		/// user and it's followers
		/// </summary>
		/// <returns></returns>
		
		public List<TweetDTO> GetAllTweets(int userId)
		{
			List<TweetDTO> retVal = new List<TweetDTO>();
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					// var followingUsersId = dbContext.Follows.Where(x => x.UserId == userId && x.Active == true).Select(x => x.FollowerId).ToList();
					var followingUsersId = dbContext.Follows.Where(x => x.FollowerId == userId && x.Active == true).Select(x => x.UserId).ToList();

					followingUsersId.Add(userId);

					// var userTweetList = dbContext.Tweets.Where(x => followingUsersId.Contains(userId) && x.Active == true).ToList();

					var userTweetList = dbContext.Tweets.Where(x => followingUsersId.Contains(x.UserId) && x.Active == true).ToList();

					//var tweetsLikedDisliked = dbContext.LikeDislikeMappers.Where(x => x.UserId == userId).ToDictionary(x => x.TweetId, y => y.Like);
					var tweetsLikedDisliked = dbContext.LikeDislikeMappers.Where(x => x.UserId == userId).ToList();//.ToDictionary(x => x.TweetId, y => y.Like);
					Dictionary<int, bool> tweetLiked = new Dictionary<int, bool>();
					for (int i = 0; i < tweetsLikedDisliked.Count(); i++)
					{
						tweetLiked[tweetsLikedDisliked[i].TweetId] = true;
					}
					retVal = this.Mapper.Map<List<TweetDTO>>(userTweetList.OrderByDescending(x => x.UpdateDate).ToList());

					retVal.ForEach(x =>
					{
						try
						{
							if (tweetLiked[x.TweetId] == true)
							{
								x.IsTweetLikedByUser = true;
								x.IsTweetDislikedByUser = false;
							}
							else
							{
								x.IsTweetLikedByUser = false;
								x.IsTweetDislikedByUser = true;
							}
						}
						catch
						{
							x.IsTweetLikedByUser = false;
							x.IsTweetDislikedByUser = false;
						}
					});
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return retVal;
		}



		public string GetMostTweetingUser()
        {
            string retVal;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var mostTweetingUser = dbContext.Database.SqlQuery<string>("select FirstName+LastName from users u join (select top(1) count(tweetId) as cnt, userId from Tweets group by userId order by cnt desc) as x on x.userId = u.userId ").FirstOrDefault();
                    retVal = mostTweetingUser.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public int AddNewTweet(TweetDTO twt)
        {
            int currid;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    Tweet tweet = new Tweet()
                    {
                        Content = twt.Content,
                        PostDate = DateTime.Now,
                        UserId = twt.UserId, //Alert
                        LikeCount = 0,
                        DislikeCount = 0,
                        Active = true,
                        UpdateDate = DateTime.Now
                    };

                    dbContext.Tweets.Add(tweet);
                    dbContext.SaveChanges();
                    currid = dbContext.Tweets.Max(s => s.TweetId);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return currid;

        }
        /// <summary>
        /// Return Tweet that's have max likes
        /// </summary>
        /// <returns></returns>
        public TweetDTO GetMostLikedTweet()
        {
            TweetDTO resTweet;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    int maxLikeCount = (int)dbContext.Tweets.Max(p => p.LikeCount);
                    var tweet = dbContext.Tweets.First(x => x.LikeCount == maxLikeCount);
                    resTweet = this.Mapper.Map<TweetDTO>(tweet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resTweet;

        }

        public bool deleteTweet(int tweetId)
        {
            //TweetDTO retVal = null;

            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var tweet = dbContext.Tweets.Where(s => s.TweetId == tweetId).FirstOrDefault();
                    tweet.Active = false;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// Return count of all tweets tweeted today
        /// </summary>
        /// <returns></returns>
        public int GetTodayTweetsCount()
        {
            int count;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var yesterdayDate = DateTime.Now.AddDays(-1);
                    var todayDate = DateTime.Now;
                    count = dbContext.Tweets.Where(c => c.PostDate > yesterdayDate &&
                    c.PostDate <= todayDate)
                    .ToList()
                    .Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }
		public bool LikeTweet(int userId, int tweetId)
		{
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var likeDislikeMapper = dbContext.LikeDislikeMappers.Where(y => y.UserId == userId && y.TweetId == tweetId).FirstOrDefault();
					var tweet = dbContext.Tweets.Where(y => y.TweetId == tweetId).FirstOrDefault();

					if (likeDislikeMapper == null)
					{
						LikeDislikeMapper likeDislikeMapperObj = new LikeDislikeMapper()
						{
							UserId = userId,
							TweetId = tweetId,
							Like = true
						};

						dbContext.LikeDislikeMappers.Add(likeDislikeMapperObj);
						tweet.LikeCount += 1;
						dbContext.SaveChanges();
					}
					else
					{
						if (likeDislikeMapper.Like == false)
						{
							likeDislikeMapper.Like = true;
							tweet.DislikeCount -= 1;
							tweet.LikeCount += 1;
							dbContext.SaveChanges();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}


		public bool DislikeTweet(int userId, int tweetId)
		{
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var likeDislikeMapper = dbContext.LikeDislikeMappers.Where(y => y.UserId == userId && y.TweetId == tweetId).FirstOrDefault();
					var tweet = dbContext.Tweets.Where(y => y.TweetId == tweetId).FirstOrDefault();

					if (likeDislikeMapper == null)
					{
						LikeDislikeMapper likeDislikeMapperObj = new LikeDislikeMapper()
						{
							UserId = userId,
							TweetId = tweetId,
							Like = false
						};

						dbContext.LikeDislikeMappers.Add(likeDislikeMapperObj);
						tweet.DislikeCount += 1;
						dbContext.SaveChanges();
					}
					else
					{
						if (likeDislikeMapper.Like == true)
						{
							likeDislikeMapper.Like = false;
							tweet.LikeCount -= 1;
							tweet.DislikeCount += 1;
							dbContext.SaveChanges();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}
		public bool updateTweet(int tweetId, string Content)
		{
			//TweetDTO retVal = null;
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var tweet = dbContext.Tweets.Where(s => s.TweetId == tweetId).FirstOrDefault();
					tweet.Content = Content;
					tweet.UpdateDate = DateTime.Now;
					dbContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}
		public string gettweet(int tweetId)
		{
			using (GlitterDBEntities dbContext = new GlitterDBEntities())
			{
				Tweet temp=dbContext.Tweets.Where((s) => s.TweetId == tweetId).FirstOrDefault();
				return temp.Content;
			}
		}

	}
}
