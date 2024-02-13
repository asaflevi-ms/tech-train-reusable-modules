using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.Tests;

[TestClass]
public class ApiValidationTests
{
    [TestMethod]
    public void ValidateApi()
    {
        var description = new ApiDescription {
            RelativePath = "/users/{userId}/סל"
        };

        var result = new ApiDescriptionValidator().Validate(description);
        Assert.AreEqual("urls should only contain ascii characters", result);
    }
}