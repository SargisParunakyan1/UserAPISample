using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UserApi.Middleware
{
    public class ExceptionHandlerMiddleWare
    {
        #region Fields

        private readonly RequestDelegate next;

        #endregion

        #region Constructor

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        #endregion

        #region Operations

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion

        #region Implementation

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            else if (ex is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            string result = JsonConvert.SerializeObject(new { error = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }

        #endregion
    }
}