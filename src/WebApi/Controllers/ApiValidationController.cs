using common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi.Controllers
{
    [ApiController]
    public class ApiValidationController : ControllerBase
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        private readonly IApiValidatorManager _apiValidatorManager;
        public ApiValidationController(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider, 
                IApiValidatorManager apiValidatorManager)
        {
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _apiValidatorManager = apiValidatorManager;
        }

        [HttpGet]
        [Route("/validateApi")]
        public IActionResult ValidateApi(int userId)  // What is userId
        {
            var validator = _apiValidatorManager;
            foreach(var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach(var item in group.Items)
                {
                     var result = validator.Validate(item);
                     if(result != null)
                     {
                        var json = JsonSerializer.Serialize(result);
                        return Ok(json);
                     }
                }
            }

            return NotFound();
        }   
    }
}
                        