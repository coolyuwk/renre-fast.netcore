using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using RenRen.Domain.Common.Logs;
using RenRen.Domain.Common.Utils;

namespace RenRen.Domain.Common.Aspect.Middlerware
{
    public static class ExceptionMiddleware
    {
        public static async Task InvokeAsync(HttpContext httpContext, Func<Task> next)
        {
            Console.WriteLine("进入处理");
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            R r;
            var type = exceptionDetails?.Error?.GetType();
            if (type?.Name == nameof(WinkSignException))
            {
                r = R.Error(code: 501, exceptionDetails?.Error?.Message);
            }
            else
            {
                //记录日志
                var client = ServiceLocator.Current.GetInstance<LogsClient>();
                await client.WriteLogsAsync(new Content()
                {
                    Message = JsonConvert.SerializeObject(exceptionDetails)
                });

                r = R.Error(exceptionDetails?.Error?.Message);
            }
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = 200;
            await System.Text.Json.JsonSerializer.SerializeAsync(httpContext.Response.Body, r);
        }
    }
}