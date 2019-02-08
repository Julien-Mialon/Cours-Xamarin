using System;
using System.Net;

namespace Common.Core.Exceptions
{
	public class DomainHttpCodeException : Exception
	{
		public int Code { get; }

		public DomainHttpCodeException(int code)
		{
			Code = code;
		}

		public DomainHttpCodeException(int code, string message) : base(message)
		{
			Code = code;
		}

		public DomainHttpCodeException(int code, string message, Exception inner) : base(message, inner)
		{
			Code = code;
		}

		public DomainHttpCodeException(HttpStatusCode code)
		{
			Code = (int)code;
		}

		public DomainHttpCodeException(HttpStatusCode code, string message) : base(message)
		{
			Code = (int)code;
		}

		public DomainHttpCodeException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
		{
			Code = (int)code;
		}
	}
}