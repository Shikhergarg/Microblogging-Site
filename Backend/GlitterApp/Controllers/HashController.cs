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
    public class HashController : ApiController
    {
        private HashBDC _hashBDC;
        public HashController()
        {
            this._hashBDC = new HashBDC();
        }

        public string GetMostTrendingHashtag()
        {
            return _hashBDC.GetMostTrendingHashtag();
        }
        public List<TweetDTO> GetAllSearchedTweets(string searchText)
        {
            //string searchHash = "elction";
            return _hashBDC.GetAllSearchedTweets(searchText);
        }
        public List<SearchUserDTO> GetAllSearchUsers(string searchText)
        {
            //string searchData = "";
            return _hashBDC.GetAllSearchUsers(searchText);
        }

    }
}
