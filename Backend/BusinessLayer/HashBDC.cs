using System.Collections.Generic;
using DataAccessLayer.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject;


namespace BusinessLayer
{
    public class HashBDC
    {
        private HashRepository _hashRepo { get; set; }

        public HashBDC()
        {
            this._hashRepo = new HashRepository();
        }
        public string GetMostTrendingHashtag()
        {
            return _hashRepo.GetMostTrendingHashtag();
        }
        public List<TweetDTO> GetAllSearchedTweets(string searchHash)
        {
            return _hashRepo.GetAllSearchedTweets(searchHash);
        }
        public List<SearchUserDTO> GetAllSearchUsers(string searchHash)
        {
            return _hashRepo.GetAllSearchUsers(searchHash);
        }
    }
}
