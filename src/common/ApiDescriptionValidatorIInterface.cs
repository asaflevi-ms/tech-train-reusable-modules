using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Patterns;

namespace TechTrain.ReusableModules.WebApi.Common;

public interface IApiValidator
{
	string Kind { get; }
	ValidationError? Validate(dynamic apiDescription);
}