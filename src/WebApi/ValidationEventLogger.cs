using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi
{
	public class ValidationEventLogger : IValidationEventLogger
	{
		private readonly IApiValidatorManager _apiValidatorManager;
		public ValidationEventLogger(IApiValidatorManager apiValidatorManager)
		{
			_apiValidatorManager = apiValidatorManager;
			_apiValidatorManager.OnApiValidatorEvent += ProcessEventFunc;
		}

		private void ProcessEventFunc(object? sender, ApiValidatorEventArgs e)
		{
			// Log the event
			Console.WriteLine(e.errorMessage);
		}
	}
}
