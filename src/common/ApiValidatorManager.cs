
namespace TechTrain.ReusableModules.WebApi.Common
{
	public interface IApiValidatorManager
	{
		ApiValidatorsManager AddApiValidator<T>() where T : IApiValidator;
		IEnumerable<ValidationError> Validate(dynamic value);

		event EventHandler<ApiValidatorEventArgs> OnApiValidatorEvent;
	}

	/// <summary>
	/// Initializing the validators, done with reflection, for every class that implmenment "IArgumentValidator"
	/// </summary>
	public class ApiValidatorsManager : IApiValidatorManager
	{
		private List<IApiValidator> _validators;
		public event EventHandler<ApiValidatorEventArgs> OnApiValidatorEvent;

		public ApiValidatorsManager()
		{
			_validators = new List<IApiValidator>();
		}


		public ApiValidatorsManager AddApiValidator<T>() where T : IApiValidator
		{
			if (_validators == null)
			{
				throw new Exception("ApiValidatorAttribute is not initialized yet.");
			}
			/// Create one instance from each type and save it in _validators dictionary.
			IApiValidator apiValidator = (IApiValidator) Activator.CreateInstance(typeof(T))!;
			_validators.Add(apiValidator);
			return this;
		}

		public IEnumerable<ValidationError> Validate(dynamic value)
		{
			List<ValidationError> errors = new List<ValidationError>();

			// Check specific type validators.
			foreach (var validator in _validators)
			{
				ValidationError err = validator.Validate(value);
				if (err != null)
				{
					OnApiValidatorEvent?.Invoke(this, new ApiValidatorEventArgs (err));
					errors.Add(err);
				}
			}

			return errors;
		}
	}
}
