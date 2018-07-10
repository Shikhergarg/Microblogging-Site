using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject;

namespace BusinessLayer
{
    public class FollowBDC
    {
        private FollowRepository _followRepo { get; set; }

        public FollowBDC()
        {
            this._followRepo = new FollowRepository();
        }

        public List<UserDTO> GetAllFollowers(int userId)
        {
			//manipulations
			return this._followRepo.GetAllFollowers(userId);
			return this._followRepo.GetAllFollowings(userId);
			
        }

        public List<UserDTO> GetAllFollowings(int userId)
        {
			//manipulations
			return this._followRepo.GetAllFollowings(userId);
		}

        public int GetFollowerCount(int userId)
        {
            return this._followRepo.FollowersCount(userId);
        }

        public int GetFollowingCount(int userId)
        {
            return this._followRepo.FollowingsCount(userId);
        }
		public bool UnFollowUser(int userId,int followingId)
		{
			return this._followRepo.UnfollowUser(userId, followingId);
		}
		public bool FollowUser(int userId, int followingId)
		{
			return this._followRepo.FollowUser(userId, followingId);
		}
	}
}
