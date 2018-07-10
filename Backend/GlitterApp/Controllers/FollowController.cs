using System;
using System.Collections.Generic;
using Shared.DataTransferObject;
using BusinessLayer;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GlitterApp.Controllers
{
    public class FollowController : ApiController
    {
        private FollowBDC _followBDC { get; set; }

        public FollowController()
        {
            this._followBDC = new FollowBDC();
        }
		[HttpGet]
        public List<UserDTO> GetAllFollowers([FromUri]int userId)
        {
            //int i = 1;
            return this._followBDC.GetAllFollowers(userId);
        }
		[HttpGet]
        public List<UserDTO> GetAllFollowing([FromUri]int userId)
        {
            
            return this._followBDC.GetAllFollowings(userId);
        }
		[HttpGet]
        public int GetFollowerCount([FromUri] int userId)
        {
            //int userId = 1;
            return this._followBDC.GetFollowerCount(userId);
        }
		[HttpGet]
        public int GetFollowingCount([FromUri]int userID)
        {
            //int userId = 1;
            return this._followBDC.GetFollowingCount(userID);
        }
		[HttpPost]
        public void FollowUser([FromBody]FollowDTO followDTO)
        {
			this._followBDC.FollowUser(followDTO.UserId, followDTO.FollowingId);
        }
		[HttpPost]
        public void UnFollowUser([FromBody]FollowDTO followDTO )
        {
			//int userId = 1;
			this._followBDC.UnFollowUser(followDTO.UserId,followDTO.FollowingId);
			//return false;
        }
    }
}
