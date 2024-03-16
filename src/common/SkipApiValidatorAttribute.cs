using common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class SkipApiValidatorAttribute : Attribute
	{ 
		public ValidationTypes TypeToSlkip { get; private set; }
		public SkipApiValidatorAttribute(ValidationTypes typeToSlkip)
		{
			TypeToSlkip = typeToSlkip;
		}
	}
}
