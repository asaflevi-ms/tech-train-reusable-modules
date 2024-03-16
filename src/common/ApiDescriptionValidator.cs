//using System.Text.RegularExpressions;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.AspNetCore.Routing.Patterns;

//namespace TechTrain.ReusableModules.WebApi.Common;

//public class ApiDescriptionValidator
//{


//	/// <summary>
//	/// Validate that the relative path is kebub-case
//	/// </summary>
//	/// <param name="apiDescription"></param>
//	/// <returns></returns>
//	public bool ValidateKebabCase(ApiDescription apiDescription)
//	{
//		if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
//			return false;
//		var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

//		// Check if Pattern pathsegments are lowercase or contains dash - = kebub-case
//		foreach (RoutePatternPathSegment seg in Pattern.PathSegments)
//		{
//			foreach (RoutePatternPart part in seg.Parts)
//			{
//				if (part.IsLiteral)
//				{
//					RoutePatternLiteralPart literalPart = (RoutePatternLiteralPart)part;
//					if (!Regex.IsMatch(literalPart.Content, @"^[a-z\-]+$"))
//					{
//						return false;
//					}
//				}
//			}
//		}

//		return true;
//	}

//	public bool ValidateQueryParametersNamingonvention(ApiDescription apiDescription)
//	{
//		if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
//			return false;
//		var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

//		// Check if Pattern pathsegments are lowercase or contains dash - = kebub-case
//		foreach (RoutePatternPathSegment seg in Pattern.PathSegments)
//		{
//			foreach (RoutePatternPart part in seg.Parts)
//			{
//				if (part.IsParameter)
//				{
//					RoutePatternParameterPart parameterPart = (RoutePatternParameterPart)part;
//					if (parameterPart.IsParameter)
//					{
//						// match camelCase
//						if (Regex.IsMatch(parameterPart.Name, @"^[a-z][a-zA-Z0-9]*$"))
//						{

//							return parameterPart.Name == "userName" || parameterPart.Name.EndsWith("Id");
//						}
//					}
//				}
//			}
//		}

//		return false;
//	}
//}
