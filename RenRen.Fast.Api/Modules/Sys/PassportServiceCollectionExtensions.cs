using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Service;
using RenRen.Fast.Api.Modules.Sys.Service.Impl;

namespace RenRen.Fast.Api.Modules.Sys
{
    public static partial class PassportServiceCollectionExtensions
    {
        public static IServiceCollection AddSysServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<ISysConfigService, SysConfigServiceImpl>();
            services.AddScoped<ISysCaptchaService, SysCaptchaServiceImpl>();
            services.AddScoped<ISysUserService, SysUserServiceImpl>();
            services.AddScoped<ISysUserTokenService, SysUserTokenServiceImpl>();
            services.AddScoped<ISysMenuService, SysMenuServiceImpl>();
            services.AddScoped<IShiroService, ShiroServiceImpl>();
            services.AddScoped<ISysRoleMenuService, SysRoleMenuServiceImpl>();
            services.AddScoped<ISysRoleService, SysRoleServiceImpl>();
            services.AddScoped<ISysUserRoleService, SysUserRoleServiceImpl>();

            #endregion

            #region validator
            services.AddTransient<IValidator<SysUser>, SysUserValidator>();
            services.AddTransient<IValidator<SysRole>, SysRoleValidator>();
            services.AddTransient<IValidator<SysConfig>, SysConfigValidator>();
            #endregion

            return services;
        }
    }
}
