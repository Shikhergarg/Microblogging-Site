using DataAccessLayer.Repository;
using FluentValidation;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations.Validator
{
	public class UserValidator : AbstractValidator<UserDTO>
    {

		public UserValidator()
		{

			RuleSet("User Registration", () =>
			{
				RuleFor(user => user.EmailId).NotEmpty().WithMessage("Required").EmailAddress().WithMessage("Invalid Email");
				RuleFor(user => user.FirstName).NotEmpty().WithMessage("Required").Matches("[a-zA-Z\\s]+$").WithMessage("Invalid Name");
				RuleFor(user => user.LastName).NotEmpty().WithMessage("Required").Matches("[a-zA-Z\\s]+$").WithMessage("Invalid Name");
				RuleFor(user => user.ImagePath).NotEmpty().WithMessage("Required");
				RuleFor(user => user.Country).NotEmpty().WithMessage("Required");
				RuleFor(user => user.Password).NotEmpty().WithMessage("Required").Length(8, 50).WithMessage("Password must be min 8 characters long").Matches(@"(?=.*\d)(?=.*[a-zA-Z])(?=.*[@#$%])").WithMessage("Password must have min 8 characters, 1 special character, 1 number and 1 alphabet");
				RuleFor(user => user.ContactNumber).NotEmpty().WithMessage("Required").Length(10).WithMessage("Invalid Number Length").Matches(@"[1-9]{1}[0-9]{9}").WithMessage("Invalid Number");

				RuleFor(x => x.EmailId).Must(BeUnique).WithMessage("Email id already exists");
				RuleFor(x => x.ImagePath).Must(ValidExtension).WithMessage("Only JPEG, JPG or PNG files are allowed");
			});

			RuleSet("User Login", () =>
			{
				RuleFor(user => user.EmailId).NotEmpty().WithMessage("Required");
				RuleFor(user => user.Password).NotEmpty().WithMessage("Required");

			});


		}

		private bool BeUnique(string email)
		{

			UserRepository userRepo = new UserRepository();
			UserDTO userDTO = userRepo.GetUserByEmailId(email);
			return userDTO == null;
		}

		private bool ValidExtension(string imagePath)
		{
			string[] words = imagePath.Split('.');
			string extension = words[words.Length - 1];
			return (extension == "" || extension.ToLower() == "jpg" || extension.ToLower() == "jpeg" || extension.ToLower() == "png");
		}



	}

}
