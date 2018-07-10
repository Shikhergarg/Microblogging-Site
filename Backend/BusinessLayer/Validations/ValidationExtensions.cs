using Shared.Validation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExitTest.BDC.Validations
{
	/// <summary>
	/// 
	/// </summary>
	public static class ValidationExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static ExitTestValidationResult ToValidationResult(this ValidationResult result)
		{
			IList<ExitTestValidationFailure> errors = new List<ExitTestValidationFailure>();

			foreach (ValidationFailure failure in result.Errors)
			{
				errors.Add(failure.ToValidationFailure());
			}

			return new ExitTestValidationResult(errors);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="failure"></param>
		/// <returns></returns>
		public static ExitTestValidationFailure ToValidationFailure(this ValidationFailure failure)
		{
			return new ExitTestValidationFailure(failure.PropertyName, failure.ErrorMessage, failure.AttemptedValue);
		}


	}
}
