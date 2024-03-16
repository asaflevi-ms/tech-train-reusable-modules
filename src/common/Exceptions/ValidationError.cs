// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Runtime.Serialization;


namespace TechTrain.ReusableModules.WebApi.Common
{
	public class ValidationError
	{
		// Source validator name
		public string Source { get; set; }

		public int Code { get; set; }
		public string Message { get; set; }
		public ValidationError InnerError { get; set; }
		public IEnumerable<ValidationError> Details { get; set; }
		public string Target { get; set; }


		/// <summary>
		/// adding a constractor with argument names that are consistent with property names, to support json deserialization of the error object
		/// </summary>
		/// <param name="code"></param>
		/// <param name="message"></param>
		/// <param name="innerError"></param>
		/// <param name="details"></param>
		/// <param name="target"></param>
		public ValidationError(string Source, int code, string message, ValidationError innerError = null, IEnumerable<ValidationError> details = null, string target = null)
		{
			this.Source = Source;
			Code = code;
			Message = message;
			InnerError = innerError;
			Details = details;
			Target = target;
		}
	}
}
