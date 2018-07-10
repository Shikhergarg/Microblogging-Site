using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
	public class SearchUserDTO
	{
		public UserDTO UserDTO { get; set; }
		public Boolean AlreadyFollowing { get; set; }
	}
}
