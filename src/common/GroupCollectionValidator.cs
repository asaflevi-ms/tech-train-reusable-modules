using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrain.ReusableModules.WebApi.Common;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class GroupCollectionValidator : IApiValidator
	{
		public string Kind => "GroupCollection";

		public ValidationError? Validate(dynamic value)
		{
		    HashSet<string> _relativePaths = new HashSet<string>();

			if (value is ApiDescriptionGroup apiDescriptionGroup)
			{
				foreach (var apiDescription in apiDescriptionGroup.Items)
				{
					if (_relativePaths.Contains(apiDescription.ActionDescriptor.DisplayName))
					{
						return new ValidationError(Kind, 400, $"Duplicate action name in path {apiDescription.ActionDescriptor.DisplayName}");
					}
					_relativePaths.Add(apiDescription.ActionDescriptor.DisplayName);
				}
			}	
			return null;
		}
	}
}
