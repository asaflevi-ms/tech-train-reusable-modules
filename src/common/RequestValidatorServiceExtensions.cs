using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public static class ApiValidatorServiceExtensions
	{
		public static ApiValidatorsManager AddApiValidator(this IServiceCollection services, IConfiguration configuration)
		{
			var apiValidators = new ApiValidatorsManager();
			services.AddSingleton<IApiValidatorManager>(apiValidators);
			return apiValidators;
		}
	}
}
