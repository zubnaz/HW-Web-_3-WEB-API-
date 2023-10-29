using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions
{

	[Serializable]
	public class HttpsException : Exception
	{
        public HttpStatusCode Status { get; set; }
        public HttpsException() { this.Status = HttpStatusCode.InternalServerError; }
		public HttpsException(string message, HttpStatusCode status) : base(message) { this.Status = status; }
		public HttpsException(string message, Exception inner, HttpStatusCode status) : base(message, inner) { this.Status = status; }
		protected HttpsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
