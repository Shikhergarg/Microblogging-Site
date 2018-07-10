using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class UserDTO : IDTO
    {
		public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
    }
}
