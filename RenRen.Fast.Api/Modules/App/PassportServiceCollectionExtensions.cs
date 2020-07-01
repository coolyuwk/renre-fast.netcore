using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.App.Service;
using RenRen.Fast.Api.Modules.App.Service.Impl;

namespace RenRen.Fast.Api.Modules.App
{
    public static partial class PassportServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<IUserService, UserServiceImpl>();

            #endregion

            #region Maps
            TinyMapper.Bind<UserDto, User>();

            #endregion

            return services;
        }
    }
}
