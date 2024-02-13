//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.AspNetCore.Routing.Patterns;

//namespace TechTrain.ReusableModules.WebApi.Controllers;

//public class ApiDescriptionValidator
//{
//    public string? Validate(ApiDescription apiDescription)
//    {
//        if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
//            return "All Api methods should have a path";

//        var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);
        
//        if (!apiDescription.RelativePath.All(char.IsAscii))
//            return "urls should only contain ascii characters";

//        return null;
//    }
//}