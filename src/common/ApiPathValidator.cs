using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Text.RegularExpressions;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi.Common
{

	public enum PathCaseTypes
	{
		KebabCase,
		camelCase
	}

	public class ApiPathValidator : IApiValidator
	{
		public string Kind => "ApiPathValidator";
		
		private readonly string _kebub_case_pattern = @"^[a-z\-]+$";
		private readonly string _camel_case_pattern = @"^[a-z][a-zA-Z0-9]*$";
		private readonly PathCaseTypes pathCaseTypes = PathCaseTypes.camelCase;
		private string _pattern = string.Empty;
		public ApiPathValidator()
		{
			_pattern = pathCaseTypes switch
			{
				PathCaseTypes.KebabCase => _kebub_case_pattern,
				PathCaseTypes.camelCase => _camel_case_pattern,
				_ => _camel_case_pattern,
			};
		}


		public ValidationError? Validate(dynamic value)
		{
            if ( value is ApiDescription apiDescription)
            {
				if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
					new ValidationError(Kind, 400, "All Api methods should have a path", target: apiDescription.RelativePath);

				var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

				// check if Pattern pathsegments contains only ascii characters
				if (!apiDescription.RelativePath.All(char.IsAscii))
					return new ValidationError(Kind, 400, $"urls should only contain ascii characters : <{apiDescription.RelativePath}>", target: apiDescription.RelativePath);

				// Check if Pattern pathsegments case
				foreach (RoutePatternPathSegment seg in Pattern.PathSegments)
				{
					foreach (RoutePatternPart part in seg.Parts)
					{
						if (part.IsLiteral)
						{
							RoutePatternLiteralPart literalPart = (RoutePatternLiteralPart)part;
							if (!Regex.IsMatch(literalPart.Content, _pattern))
							{
								//return $"{literalPart.Content} path must be {pathCaseTypes.ToString()}";
								return new ValidationError(Kind, 400, $"{literalPart.Content} path must be {pathCaseTypes.ToString()}", target: apiDescription.RelativePath);
							}

						}
					}
				}
			}
			return null;
		}
	}
}
