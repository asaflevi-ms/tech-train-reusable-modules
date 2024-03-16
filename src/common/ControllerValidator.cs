using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Text.RegularExpressions;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class ControllerValidator : IApiValidator
	{
		public string Kind => "ControllerValidator";
		public ControllerValidator()
		{
		}
		public ValidationError? Validate(dynamic value)
		{
			if (value is ApiDescription apiDescription)
			{
				ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)apiDescription.ActionDescriptor;

				// validate that the response body is json structured
				if (apiDescription.SupportedResponseTypes.Count > 0)
				{
					foreach (var responseType in apiDescription.SupportedResponseTypes)
					{
						if (responseType.Type != null)
						{
							// if type contains DataContractAttribute or DataMemberAttribute return error
							if (responseType.Type.GetCustomAttributes(typeof(System.Runtime.Serialization.DataContractAttribute), true).Length == 0)
							{
								return new ValidationError(Kind, 400, $"Response type {responseType.Type.ToString()} should be structured / serializable");
							}
						}
					}
				}
			}

			return null;
		}
	}
}
