using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Attributes
{
	public class ResponseAttribute : ProducesResponseTypeAttribute
	{
		public ResponseAttribute(Type type, HttpStatusCode statusCode) : base(type, (int)statusCode)
		{
		}
	}
}