using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Text.RegularExpressions;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class PathParameterValidator : IApiValidator
	{
		public string Kind => "PathParameterValidator";

		public PathParameterValidator()
		{
		}
		public ValidationError? Validate(dynamic value)
		{
			if (value is ApiDescription apiDescription)
			{
				if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
					return new ValidationError(Kind, 400, "All Api methods should have a path", target: apiDescription.RelativePath);
				var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

				// Check if Pattern pathsegments are lowercase or contains dash - = kebub-case
				foreach (RoutePatternPathSegment seg in Pattern.PathSegments)
				{
					foreach (RoutePatternPart part in seg.Parts)
					{
						if (part.IsParameter)
						{
							RoutePatternParameterPart parameterPart = (RoutePatternParameterPart)part;
							if (parameterPart.IsParameter)
							{
								// match camelCase
								if (Regex.IsMatch(parameterPart.Name, @"^[a-z][a-zA-Z0-9]*$"))
								{
									// Check if parameter name ends with Id
									if (!parameterPart.Name.EndsWith("Id"))
										return new ValidationError(Kind, 400, $"Parameter <{parameterPart.Name}> must end with Id", target: apiDescription.RelativePath);
								}
								else
								{
									return new ValidationError(Kind, 400, $"Parameter <{parameterPart.Name}> name must be camelCase", target: apiDescription.RelativePath);
								}
							}
						}
					}
				}
			}

			return null;
		}
	}
}
