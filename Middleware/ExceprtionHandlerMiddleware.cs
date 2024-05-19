using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace EcommerceAPI.Middleware
{
	public class ExceprtionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly RequestDelegate _next;

		public ExceprtionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				var errorId = Guid.NewGuid();
				// log exception
				_logger.LogError(ex, $"{errorId} : {ex.Message}");
				// return customer response
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var error = new
				{
					Id = errorId,
					Error = ex.Message//"Something went wrong! we are lookign into resolving this."
				};

				await httpContext.Response.WriteAsJsonAsync(error);
			}
		}
	}
}

