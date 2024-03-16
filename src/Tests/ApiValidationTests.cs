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

        var result = new ApiPathValidator().Validate(description);
        Assert.AreEqual("urls should only contain ascii characters", result.Message);
    }

	/// <summary>
	/// This test validates url case.
	/// Urls path segments must be lower case, kebub case
	/// </summary>
	/// <param name="inputUrl"></param>
	/// <param name="expectedResult"></param>
	[TestMethod]
	[DataRow("/this-is/valid/url", null)]
	[DataRow("/this-is/inValid/url", "url path must be kebub-case")]
	public void ValidateLowerCaseUrls (string inputUrl, string expectedResult)
	{
		var description = new ApiDescription
		{
			RelativePath = inputUrl
		};

		var result = new ControllerValidator().Validate(description);
		Assert.AreEqual(expectedResult, result.Message);
	}
	/// <summary>
	/// This test validates url case.
	/// Urls path segments must be lower case, kebub case
	/// </summary>
	/// <param name="inputUrl"></param>
	/// <param name="expectedResult"></param>
	[TestMethod]
	[DataRow("/this-is/valid/{userName}/url", true, "valid")]
	[DataRow("/this-is/valid/{UserId}/url", false, "Parameter name must be camelCase")]
	[DataRow("/this-is/valid/{UserName}/url", false, "Parameter name must end with Id")]
	public void ValidateParametersConvention(string inputUrl, string expectedResult)
	{
		var description = new ApiDescription
		{
			RelativePath = inputUrl
		};

		var result = new PathParameterValidator().Validate(description);
		Assert.AreEqual(expectedResult, result.Message);
	}
}
