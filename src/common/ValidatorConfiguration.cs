using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace common
{

	[DataContract]
	public enum ValidationTypes
	{
		[DataMember(Name = "validateParameters")]
		ValidateParameters,
		[DataMember(Name = "validatePath")]
		ValidatePath,
		[DataMember(Name = "all")]
		All,
	}

	[DataContract]
	public class ValidatorConfiguration
	{
		[DataMember (Name = "validatorSteps")]
		public List<ValidationTypes> ValidatorSteps;
	}
}
