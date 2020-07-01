using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Namotion.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using RenRen.Domain.Auth;
using RenRen.Domain.Auth.Impl;
using RenRen.Domain.Config;
using Microsoft.Extensions.Options;

namespace RenRen.Domain.Common.Aspect.Middlerware
{
    /// <summary>
    /// 自定义授权验证特性
    /// </summary>
    public class RequiresPermissionsAttribute : TypeFilterAttribute
    {
        public RequiresPermissionsAttribute(ClaimType claimType, string claimValue = "") : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType.ToString(), claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        readonly IEnumerable<IAuthService> _authService;
        readonly RenRenSettings _renRenSettings;

        public ClaimRequirementFilter(Claim claim, IEnumerable<IAuthService> authService, IOptions<RenRenSettings> renRenSettings)
        {
            _claim = claim;
            _authService = authService;
            _renRenSettings = renRenSettings.Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //检查白名单
            if (_renRenSettings.WhiteList != null && _renRenSettings.WhiteList.Any(path => path == context.HttpContext.Request.Path.Value))
            {
                return;
            }
            ClaimType claimType = Enum.Parse<ClaimType>(_claim.Type);
            bool permission = false;

            string token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            permission = claimType switch
            {
                ClaimType.Oauth2 => _authService.First(a => a.ServiceName == nameof(OauthAuthServiceImpl)).PermissionAsync(token, _claim.Value).Result,
                ClaimType.JWT => _authService.First(a => a.ServiceName == nameof(JwtAuthServiceImpl)).PermissionAsync(token, _claim.Value).Result,
                _ => throw new WinkSignException($"没有指定的授权方式：{claimType}"),
            };

            if (!permission)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string GetToken()
            {
                string token = context.HttpContext.Request.Headers["token"];
                if (string.IsNullOrEmpty(token))
                {
                    token = context.HttpContext.Request.Query["token"];
                }
                return token;
            }
        }
    }

    public enum ClaimType
    {
        Oauth2,
        JWT
    }
}
