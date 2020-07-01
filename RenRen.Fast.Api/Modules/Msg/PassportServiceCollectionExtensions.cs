using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity.Msg;
using RenRen.Fast.Api.Modules.Msg.Cloud;
using RenRen.Fast.Api.Modules.Msg.Form;
using RenRen.Fast.Api.Modules.Msg.Service;
using RenRen.Fast.Api.Modules.Msg.Service.Impl;

namespace RenRen.Fast.Api.Modules.Msg
{
    public static partial class PassportServiceCollectionExtensions
    {
        public static IServiceCollection AddSmgServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ICloudSmsService, TencentSmsServiceImpl>();
            services.AddScoped<ISmsService, SmsCaptchaServiceImpl>();
            #endregion

            #region Maps
            TinyMapper.Bind<SmsDto, MsgSms>();
            #endregion
            return services;
        }
    }
}
