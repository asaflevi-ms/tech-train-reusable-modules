using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TechTrain.ReusableModules.WebApi.Common;

namespace Webapi.Extensions
{
	public class NoopValidator : IApiValidator
	{
		public string Kind => "NoopValidator";

		public ValidationError? Validate(dynamic value)
		{
			if (value is ApiDescription apiDescription)
			{
				apiDescription.RelativePath = apiDescription?.RelativePath?.ToLower();
				//return new ValidationError(Kind, 400, "NoopValidator", target: apiDescription.RelativePath);
			}
			return null;
		}
	}
}
