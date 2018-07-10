using BusinessLayer.Validations.Validator;
using DataAccessLayer.Repository;
using ExitTest.Shared.DTO;
using FinalExitTest.BDC.Validations;
using Shared.DataTransferObject;
using Shared.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBDC
    {
        private UserRepository _userRepo { get; set; }

        public UserBDC()
        {
            this._userRepo = new UserRepository();
        }

        public List<UserDTO> GetAllUser()
        {
            //manipulations
            return this._userRepo.GetAllUsers();
        }
		public List<ErrorDTO> RegisterUser(UserDTO usr, string imagePath)
		{
			List<ErrorDTO> errorList = null;
			UserDTO user = new UserDTO
			{
				EmailId = usr.EmailId,
				FirstName = usr.FirstName,
				LastName = usr.LastName,
				ContactNumber = usr.ContactNumber,
				Country = usr.Country,
				Password = usr.Password,
				ImagePath = imagePath
			};
			ExitTestValidationResult validationResult = Validator<UserValidator, UserDTO>.Validate(user, ruleSet: "User Registration");
			if (validationResult.IsValid)
			{

				//int tweetId = this._usrRepo.AddNewUser(user);
				this._userRepo.RegisterUser(user);
			}
			else
			{
				errorList = new List<ErrorDTO>();
				ErrorDTO errorDTO = null;
				foreach (var item in validationResult.Errors)
				{
					errorDTO = new ErrorDTO();
					errorDTO.ErrorMessage = item.ErrorMessage;
					errorDTO.PropertyName = item.PropertyName;
					errorList.Add(errorDTO);
				}
			}

			return errorList;

		}

		public LoginDTO Login(string emailId, string password)
		{
			LoginDTO loginDTO = new LoginDTO();
			List<ErrorDTO> errorList = null;

			UserDTO usr = new UserDTO
			{
				EmailId = emailId,
				Password = password
			};
			ExitTestValidationResult validationResult = Validator<UserValidator, UserDTO>.Validate(usr, ruleSet: "User Login");
			if (validationResult.IsValid)
			{
				loginDTO.UserDTO = this._userRepo.Login(emailId, password);
				if (loginDTO.UserDTO == null)
				{
					errorList = new List<ErrorDTO>();
					ErrorDTO errorDTO = new ErrorDTO
					{
						ErrorMessage = "Email id and password do not match",
						PropertyName = "Invalid Credentials"
					};
					errorList.Add(errorDTO);
					//loginDTO.ErrorDTOList = errorList;
				}
			}
			else
			{
				errorList = new List<ErrorDTO>();
				ErrorDTO errorDTO = null;
				foreach (var item in validationResult.Errors)
				{
					errorDTO = new ErrorDTO();
					errorDTO.ErrorMessage = item.ErrorMessage;
					errorDTO.PropertyName = item.PropertyName;
					errorList.Add(errorDTO);
				}
				//loginDTO.ErrorDTOList = errorList;
			}
			loginDTO.ErrorDTOList = errorList;
			return loginDTO;
		}

	}
}
