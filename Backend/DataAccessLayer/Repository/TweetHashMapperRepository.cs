using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TweetHashMapperRepository
    {
        public List<TweetHashMapper> updateTweetHashMapper(int tweetId)
        {
            //TweetDTO retVal = null;
            List<TweetHashMapper> mapperList = new List<TweetHashMapper>();
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var tweetHashMappersList = dbContext.TweetHashMappers.Where(s => s.TweetId == tweetId).ToList();
                    mapperList.AddRange(tweetHashMappersList);
                    //tweetHashMappersList.RemoveAll(x => x.TweetId == tweetId);
                    for (int i = 0; i < tweetHashMappersList.Count; i++)
                    {
                        dbContext.TweetHashMappers.Remove(tweetHashMappersList[i]);
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mapperList;
        }
    }
}
