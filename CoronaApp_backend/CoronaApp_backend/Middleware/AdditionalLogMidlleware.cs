using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Serilog;

namespace CoronaApp_backend.Middleware
{
	public class AdditionalLogMidlleware
	{
		readonly RequestDelegate _next;
		public AdditionalLogMidlleware(RequestDelegate next) { _next = next; }

		public Task Invoke(HttpContext httpContext)
		{
			HttpRequest request = httpContext.Request;

			if (request.ContentLength != null)	//if there is a request body
			{
				IHttpBodyControlFeature? syncIOFeature = httpContext.Features.Get<IHttpBodyControlFeature>();	//for sync .Read()
				if (syncIOFeature != null)
					syncIOFeature.AllowSynchronousIO = true;

				HttpRequestRewindExtensions.EnableBuffering(request); //.CanSeek = true

				byte[] requestBytes = new byte[(int)request.ContentLength];
				request.Body.Read(requestBytes);

				#region new log entry
				string requestBody = Encoding.UTF8.GetString(requestBytes);
				requestBody = requestBody.Replace("\n", "\r\n");
				Log.Information(@"Request Body: content-type: {0}{1}{2}", request.ContentType, Environment.NewLine, requestBody);
				#endregion

				request.Body.Position = 0;
			}

			if (request.Method != HttpMethod.Get.ToString())	//response body will not be read for a GET
			{
				HttpResponse response = httpContext.Response;
				Stream originalStream = response.Body;		//ref to original HttpResponseStream

				MemoryStream memStream = new MemoryStream();
				response.Body = memStream;

				_next(httpContext);

				if (response.Body.Length >0)	//if there is a response body
				{
					
					response.Body.Position = 0;
					byte[] responseBytes = new byte[(int)response.Body.Length];
					response.Body.Read(responseBytes);

					#region new log entry
					string responseBody = Encoding.UTF8.GetString(responseBytes);
					responseBody = responseBody.Replace("{", "\r\n{\r\n  ");
					responseBody = responseBody.Replace(":", " : ");
					responseBody = responseBody.Replace(",", ",\r\n  ");
					responseBody = responseBody.Replace("}", "\r\n}");
					Log.Information(@"Response Body: content-type: {0}{1}{2}", response.ContentType, Environment.NewLine, responseBody);
					#endregion

					response.Body.Position = 0;
				}

				memStream.CopyTo(originalStream);
				return Task.FromResult(httpContext);
			}

			return Task.FromResult(_next(httpContext));
		}
	}

	public static class AdditionalLogMidllewareExtensions
	{
		public static IApplicationBuilder UseAdditionalLogMidlleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AdditionalLogMidlleware>();
		}
	}
}
