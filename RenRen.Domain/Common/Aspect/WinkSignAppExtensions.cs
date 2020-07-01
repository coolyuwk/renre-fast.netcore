using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RenRen.Domain.Common.Aspect.Middlerware;

namespace RenRen.Domain.Common.Aspect
{
    public static class WinkSignAppExtensions
    {
        public static IApplicationBuilder UseWinkSign(this IApplicationBuilder app, IConfiguration configuration)
        {
            //设置基础路由ContextPath
            var config = configuration.GetValue<string>("RenRenSettings:ContextPath");

            if (config != null)
            {
                app.UsePathBase(new Microsoft.AspNetCore.Http.PathString($"/{config}"));
            }
            app.UseExceptionHandler(handler =>
            {
                handler.Use(ExceptionMiddleware.InvokeAsync);
            });
            app.UseFileServer();
            app.UseRouting();
            app.UseCors();
            app.UseMiddleware(typeof(LoginMiddlerware));
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }
    }
}
