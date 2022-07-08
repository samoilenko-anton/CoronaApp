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
			if (request.Method == HttpMethod.Get.ToString() | request.Method == HttpMethod.Delete.ToString())
				return _next(httpContext);

			IHttpBodyControlFeature? syncIOFeature = httpContext.Features.Get<IHttpBodyControlFeature>();
			if (syncIOFeature != null)
				syncIOFeature.AllowSynchronousIO = true;

			HttpRequestRewindExtensions.EnableBuffering(request);

			byte[] bytes = new byte[(int)request.ContentLength];
			request.Body.Read(bytes);
			string requestBody = Encoding.UTF8.GetString(bytes);
			requestBody.Replace("\n", "\r\n");

			request.Body.Seek(0, SeekOrigin.Begin);
			
			Log.Information(@"Request Body: content-type: {0}{1}{2}", request.ContentType, Environment.NewLine, requestBody);

			return _next(httpContext);
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