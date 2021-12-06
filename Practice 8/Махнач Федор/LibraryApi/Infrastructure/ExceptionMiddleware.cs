using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LibraryApi.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryApi.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await WrapInHttpError(e, context);
            }
        }

        private static async Task WrapInHttpError(Exception exception, HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = GetStatusCodeByException(exception);
            context.Response.ContentType = "application/json";

            var errorObject = new ResponseError(exception.Message);

            var json = JsonConvert.SerializeObject(errorObject);
            await context.Response.WriteAsync(json);
        }

        private static int GetStatusCodeByException(Exception exception)
        {
            return exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                DomainException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}