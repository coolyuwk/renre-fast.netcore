using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSwag;
using RenRen.Domain.Auth;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Auth.Impl;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Logs;
using RenRen.Domain.Config;

namespace RenRen.Domain.Common.Aspect
{
    public static class WinkSignServiceCollectionExtensions
    {
        public static IServiceCollection AddWinkSignServices(this IServiceCollection services, IConfiguration configuration, ApiSettings apiSettings)
        {
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //跨域
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .SetIsOriginAllowed(t => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.AddDbContext<AuthDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("passport"));
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        CustomBadRequest problems = new CustomBadRequest(context);
                        return new BadRequestObjectResult(problems);
                    };
                })
                .AddFluentValidation();


            services.AddSwaggerDocument(option =>
            {
                option.PostProcess = (doc) =>
                {
                    doc.Info.Version = apiSettings.Version;
                    doc.Info.Title = apiSettings.Title;
                    doc.Info.Contact = new OpenApiContact()
                    {
                        Email = apiSettings.Email,
                        Name = apiSettings.Name
                    };
                };
            });
            services.AddSwaggerGenNewtonsoftSupport();
            //登录用户
            services.AddScoped<LoginUser>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //注入配置类
            services.Configure<RenRenSettings>(configuration.GetSection("RenRenSettings"));
            //鉴权服务
            services.AddSingleton<RoleMenuCollection>();
            services.AddScoped<IAuthService, OauthAuthServiceImpl>();
            services.AddScoped<IAuthService, JwtAuthServiceImpl>();
            //LogHttpClient
            services.AddHttpClient<LogsClient>();
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            return services;
        }
    }


    public class ApiSettings
    {
        public ApiSettings() { }
        public ApiSettings(string version, string name, string title, string email)
        {
            Version = version;
            Name = name;
            Title = title;
            Email = email;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public string Email { get; set; }
    }
}
