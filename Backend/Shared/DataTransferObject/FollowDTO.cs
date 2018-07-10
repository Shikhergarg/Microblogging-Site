using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class FollowDTO : IDTO
    {
        public int UserId { get; set; }
        public int FollowingId { get; set; }
    }
}
