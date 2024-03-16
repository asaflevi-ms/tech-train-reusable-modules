using System.Text.Json;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi
{
	public class ValidationEventLogger 
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
			Console.WriteLine(JsonSerializer.Serialize(e.errorMessage));
		}
	}
}
