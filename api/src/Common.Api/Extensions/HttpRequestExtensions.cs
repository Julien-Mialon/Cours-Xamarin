using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Common.Api.Extensions
{
	public static class HttpRequestExtensions
	{
		public static string GetAuthenticationToken(this IHttpContextAccessor contextAccessor, string headerName, string tokenType, string queryStringParameterName)
		{
			return contextAccessor.HttpContext.Request.GetAuthenticationToken(headerName, tokenType, queryStringParameterName);
		}
		
		public static string GetAuthenticationToken(this IHttpContextAccessor contextAccessor, string headerName, string queryStringParameterName)
		{
			return contextAccessor.GetAuthenticationToken(headerName, null, queryStringParameterName);
		}
		
		public static string GetAuthenticationToken(this HttpRequest request, string headerName, string tokenType, string queryStringParameterName)
		{
			try
			{
				IHeaderDictionary headers = request.Headers;
				string authorizationValue;

				if (headers.ContainsKey(headerName))
				{
					authorizationValue = headers[headerName].ToString();	
				}
				else
				{
					IQueryCollection queryString = request.Query;
					if (!queryString.ContainsKey(queryStringParameterName))
					{
						return null;
					}

					authorizationValue = queryString[queryStringParameterName].ToString();
				}

				authorizationValue = authorizationValue.Trim();
				if (string.IsNullOrEmpty(authorizationValue))
				{
					return null;
				}

				if (tokenType == null)
				{
					return authorizationValue;
				}
				
				if (!authorizationValue.StartsWith(tokenType))
				{
					return null;
				}

				return authorizationValue.Substring(tokenType.Length).Trim();
			}
			catch
			{
				return null;
			}
		}
		
		public static string GetAuthenticationToken(this HttpRequest request, string headerName, string queryStringParameterName)
		{
			return request.GetAuthenticationToken(headerName, null, queryStringParameterName);
		}

		public static string LangIsoCode(this HttpRequest request)
		{
			var feature = request.HttpContext.Features.Get<IRequestCultureFeature>();
			return feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLowerInvariant();
		}
	}
}