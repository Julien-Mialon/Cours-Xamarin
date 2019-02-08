using System;

namespace Common.Api.Attributes
{
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class ErrorCodeAttribute : Attribute
	{
		public string ErrorCode { get; }
		public string Explanation { get; }

		public ErrorCodeAttribute(string errorCode)
		{
			ErrorCode = errorCode;
		}
		
		public ErrorCodeAttribute(string errorCode, string explanation) : this(errorCode)
		{
			Explanation = explanation;
		}
	}
}