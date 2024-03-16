

using System.Net;

namespace TechTrain.ReusableModules.WebApi.Common
{
	public class ValidationExceptionBase : Exception
	{
		public HttpStatusCode StatusCode { get; private set; }
		public ValidationError Error { get; internal set; }

		public ValidationExceptionBase(HttpStatusCode statusCode, 
										ValidationError error,
										string message, 
										string target = null,
										ValidationError innerError = null,
										IEnumerable<ValidationError> details = null,
										Exception innerException = null)
										: base(message)
									{
										StatusCode = statusCode;
										Error = error;
									}

		public ValidationExceptionBase(string source, string message)
			: base(message)
		{
			StatusCode = HttpStatusCode.InternalServerError;
			Error = new ValidationError(source, -1, message);
		}

		public ValidationExceptionBase(ValidationError error)
			: base(error.Message)
		{
			Error = error;
		}
	}
}
