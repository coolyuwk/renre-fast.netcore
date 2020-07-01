using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.App;
using RenRen.Fast.Api.Modules.Msg;
using RenRen.Fast.Api.Modules.Oss;
using RenRen.Fast.Api.Modules.Sys;
using RenRen.Domain.Common.Aspect;

namespace RenRen.Fast.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region ����
            services.AddWinkSignServices(Configuration, new ApiSettings(version: "V1.0", name: "��̶̶", title: "RenRen-Fast .NetCore", email: "ziybb@qq.com"));
            #endregion

            #region ���ݿ�
            services.AddDbContext<PassportDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("passport"));
            });


            #endregion

            #region ҵ�����
            services.AddSysServices();
            services.AddSmgServices();
            services.AddCloudServices();
            services.AddAppServices();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PassportDbContext passportDbContext)
        {
            passportDbContext.Database.Migrate();

            app.UseWinkSign(Configuration);
        }
    }
}
