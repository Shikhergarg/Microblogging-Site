using BusinessLayer;
using ExitTest.Shared.DTO;
using Newtonsoft.Json;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlitterApp.Controllers
{
    public class UserController : ApiController
    {
        private UserBDC _userBDC { get; set; }

        public UserController()
        {
            this._userBDC = new UserBDC();
        }

        public List<UserDTO> GetAllUser()
        {
            return this._userBDC.GetAllUser();
        }
		[HttpPost]
		public async Task<IEnumerable<ErrorDTO>> RegisterUser()
		{
			List<ErrorDTO> errorDTOList = null;
			if (Request.Content.IsMimeMultipartContent())
			{

				try
				{
					string fullPath = HttpContext.Current.Server.MapPath("~/images");
					var streamProvider = new CustomMultipartFormDataStreamProvider(fullPath);
					var result = await Request.Content.ReadAsMultipartAsync(streamProvider);
					var newUser = JsonConvert.DeserializeObject<UserDTO>(streamProvider.FormData["NewUser"]);
					var fname = result.FileData[0].LocalFileName;

					FileInfo fi = new FileInfo(fname.ToString());

					errorDTOList = new List<ErrorDTO>();
					errorDTOList = this._userBDC.RegisterUser(newUser, "images/" + fi.Name);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return errorDTOList;
		}

		[HttpPost]
		public LoginDTO Login([FromBody] UserDTO user)
		{
			return this._userBDC.Login(user.EmailId, user.Password);
		}


	}
	public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
	{
		public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

		public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
		{
			string fileName;
			if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
			{
				var ext = Path.GetExtension(headers.ContentDisposition.FileName.Replace("\"", string.Empty));
				fileName = Guid.NewGuid() + ext;
			}
			else
			{
				fileName = Guid.NewGuid() + ".data";
			}
			return fileName;
		}
	}


}
