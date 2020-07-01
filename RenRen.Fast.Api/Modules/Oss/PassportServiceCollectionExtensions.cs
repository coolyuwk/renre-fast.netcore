using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Modules.Oss.Cloud;
using RenRen.Fast.Api.Modules.Oss.Service;
using RenRen.Fast.Api.Modules.Oss.Service.Impl;

namespace RenRen.Fast.Api.Modules.Oss
{
    public static partial class PassportServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudServices(this IServiceCollection services)
        {
            services.AddScoped<ISysOssService, SysOssServiceImpl>();
            services.AddScoped(typeof(OSSFactory));
            return services;
        }
    }
}
