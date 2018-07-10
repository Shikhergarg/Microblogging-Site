using ExitTest.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
	public class LoginDTO
	{
		public List<ErrorDTO> ErrorDTOList { get; set; }
		public UserDTO UserDTO { get; set; }
	}
}