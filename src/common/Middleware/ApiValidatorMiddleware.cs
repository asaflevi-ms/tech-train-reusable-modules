using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using TechTrain.ReusableModules.WebApi.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class ApiValidatorMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IApiValidatorManager _apiValidatorManager;

		public ApiValidatorMiddleware(
			RequestDelegate next,
			IApiValidatorManager apiValidatorManager)
		{
			_next = next;
			_apiValidatorManager = apiValidatorManager;
		}

		public async Task Invoke(HttpContext httpContext, IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider)
		{
			if (!RequestingApiValidation(httpContext.Request))
			{
				await _next(httpContext);
				return;
			}

			try
			{
				var basePath = httpContext.Request.PathBase.HasValue
					? httpContext.Request.PathBase.Value
					: null;

				// Creare ApiDescriptor from the request
				List<ValidationError> errors = new List<ValidationError>();
				foreach (var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
				{
					var result = _apiValidatorManager.Validate(group);
					if (result != null)
						// add results to errors
						errors.AddRange(result);

					foreach (var item in group.Items)
					{
					    result = _apiValidatorManager.Validate(item);
						if (result != null)
							// add results to errors
							errors.AddRange(result);
					}
				}
				ResponseWithValidationContent(httpContext.Response, errors);
			}
			catch (Exception)
			{
				RespondWithNotFound(httpContext.Response);
			}
		}

		private bool RequestingApiValidation(HttpRequest request)
		{
			if (request.Method != "GET") return false;
			// if request RelativePath != "ValidateApi" return false
			if (request.Path != "/validateApi") return false;

			return true;
		}

		private void RespondWithNotFound(HttpResponse response)
		{
			response.StatusCode = 404;
		}

		private async Task ResponseWithValidationContent(HttpResponse response, IEnumerable<ValidationError> errors)
		{
			response.StatusCode = 200;
			response.ContentType = "application/json;charset=utf-8";

			using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				var jsonWriter = new OpenApiJsonWriter(textWriter);
	
				string json = JsonSerializer.Serialize(errors);

				await response.WriteAsync(json, new UTF8Encoding(false));
			}
		}
	}
}