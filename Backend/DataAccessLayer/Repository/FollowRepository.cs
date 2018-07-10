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
    public class FollowRepository : RepositoryBase
    {
        public int FollowersCount(int userId)
        {
            //List<UserDTO> retVal = null;
            using (GlitterDBEntities dbContext = new GlitterDBEntities())
            { 
                return dbContext.Follows.Where(x => x.UserId == userId && x.Active).ToList().Count;
                //retVal = follow.ToList();
            }
            //return (follow.Count);
        }

        public int FollowingsCount(int userId)
        {
            //List<UserDTO> retVal = null;
            using (GlitterDBEntities dbContext = new GlitterDBEntities())
            {
                return dbContext.Follows.Where(x => x.FollowerId == userId && x.Active).ToList().Count();
                //retVal = follow != null && follow.Count > 0 ? new List<UserDTO>() : null;
            }
            //return (retVal.Count);
        }

        public List<UserDTO> GetAllFollowings(int userId)
        {
			List<UserDTO> retVal = null;

			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					//var follow = dbContext.Follows.Where(x => x.UserId == i && x.Active).ToList();
					//retVal = follow != null && follow.Count > 0 ? new List<FoellowDTO>() : null;
					var list = dbContext.Database.SqlQuery<UserDTO>("select u.UserId,u.FirstName,u.LastName,u.ImagePath" +
						" from Users u Join Follow f on f.FollowerId=" + userId + " where f.UserID=u.USerId AND Active=1").ToList();

					retVal = this.Mapper.Map<List<UserDTO>>(list);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return retVal;
		}

        public List<UserDTO> GetAllFollowers(int UserId)
        {
            //List<FollowDTO> retVal = null;
            List<UserDTO> retVal = null;
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    //var following = dbContext.Follows.Where(x => x.FollowerId== i && x.Active).ToList();
                    //retVal = following != null && following.Count > 0 ? new List<FollowDTO>() : null;
                    //retVal = this.Mapper.Map<List<FollowDTO>>(following);
                    retVal = dbContext.Database.SqlQuery<UserDTO>("select u.UserId,u.FirstName,u.LastName,u.ImagePath"+
                        " from Users u Join Follow f on u.UserId=f.FollowerId where f.UserID="+UserId +"AND Active=1").ToList();
                    
                    retVal = this.Mapper.Map<List<UserDTO>>(retVal);
                }
            }
            catch (Exception ex)
            {
              throw ex;
            }

            return retVal;
        }

        /// <summary>
        /// Follow User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followingId"></param>
        /// <returns></returns>
        public bool FollowUser(int userId, int followingId)
        {
            try
            {
                using (GlitterDBEntities dbContext = new GlitterDBEntities())
                {
                    var result = dbContext.Follows.Where(p => p.UserId == followingId && p.FollowerId == userId).FirstOrDefault();
                    if (result == null)
                    {
                        Follow follow = new Follow()
                        {
                            UserId = followingId,
                            FollowerId = userId,
                            Active = true
                        };
                        dbContext.Follows.Add(follow);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        if (result.Active == false)
                        {
                            result.Active = true;
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

		/// <summary>
		/// Unfollow User
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="followingId"></param>
		/// <returns></returns>
		public bool UnfollowUser(int userId, int followingId)
		{
			try
			{
				using (GlitterDBEntities dbContext = new GlitterDBEntities())
				{
					var follow = dbContext.Follows.Where(p => p.UserId == followingId && p.FollowerId == userId).FirstOrDefault();
					if (follow != null)
					{
						follow.Active = false;
						dbContext.SaveChanges();
					}
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
