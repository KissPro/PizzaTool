using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pizza.Utilities.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (error.InnerException is FlurlHttpException fex)
                {
                    response.StatusCode = fex.StatusCode.GetValueOrDefault();
                }
                else
                {
                    switch (error)
                    {
                        case HandleException e:
                            // custom application error
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case KeyNotFoundException e:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        default:
                            // unhandled error
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                }
                // Return log
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                // Log message
                Log.Error("Something went wrong: " + error?.Message);
                Log.Error(error?.StackTrace);
                await response.WriteAsync(result);
            }
        }
    }
}
