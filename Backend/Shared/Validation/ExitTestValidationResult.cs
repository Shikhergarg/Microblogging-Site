using System.Collections.Generic;

namespace Shared.Validation
{
	public class ExitTestValidationResult
	{
		public IList<ExitTestValidationFailure> Errors { get; private set; }

		public bool IsValid
		{
			get { return Errors == null || Errors.Count == 0; }
		}

		public ExitTestValidationResult(IList<ExitTestValidationFailure> failures)
		{
			Errors = failures;
		}

		public ExitTestValidationResult()
		{
			Errors = new List<ExitTestValidationFailure>();
		}
	}

	public class ExitTestValidationFailure
	{
		public object AttemptedValue { get; private set; }
		public object CustomState { get; set; }
		public string ErrorMessage { get; private set; }
		public string PropertyName { get; set; }

		public ExitTestValidationFailure(string propertyName, string error)
		{
			PropertyName = propertyName;
			ErrorMessage = error;
		}

		public ExitTestValidationFailure(string propertyName, string error, object attemptedValue)
		{
			PropertyName = propertyName;
			ErrorMessage = error;
			AttemptedValue = attemptedValue;
		}
	}
}
