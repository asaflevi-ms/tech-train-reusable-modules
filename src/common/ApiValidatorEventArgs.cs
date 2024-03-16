namespace TechTrain.ReusableModules.WebApi.Common
{
	public class ApiValidatorEventArgs
	{
		public ApiValidatorEventArgs(ValidationError errorMessage)
		{
			this.errorMessage = errorMessage;
		}
		public ValidationError errorMessage { get; private set; }
	}
}