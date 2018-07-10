using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class TweetDTO : IDTO
    {
        public int TweetId { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int UserId { get; set; }
        public UserDTO user { get; set; }
        public bool Active { get; set; }
        public bool IsTweetLikedByUser { get; set; }
        public bool IsTweetDislikedByUser { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
