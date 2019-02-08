using System;

namespace Common.Core.Exceptions
{
	public class DomainException : Exception
	{
		public string ErrorCode { get; }

		public string ErrorMessage { get; }

		public DomainException(string errorCode, string errorMessage) : base(errorMessage)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}

		public DomainException(string errorCode, string errorMessage, Exception inner) : base(errorMessage, inner)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}
	}
}