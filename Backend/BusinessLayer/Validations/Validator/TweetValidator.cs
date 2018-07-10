using Shared.DataTransferObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations.Validator
{

	public class TweetValidator : AbstractValidator<TweetDTO>
	{
		public TweetValidator()
		{

			RuleSet("Compose Tweet", () =>
			{
				RuleFor(tweet => tweet.Content).NotEmpty().WithMessage("Please write something...").Length(1, 240);
			});
		}
	}


}