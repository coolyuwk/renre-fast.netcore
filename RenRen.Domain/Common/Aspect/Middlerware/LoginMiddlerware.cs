using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using RenRen.Domain.Auth.Impl;
using System.Collections.Generic;
using RenRen.Domain.Auth;
using System;

namespace RenRen.Domain.Common.Aspect.Middlerware
{
    public sealed class LoginMiddlerware
    {
        private readonly RequestDelegate _next;

        public LoginMiddlerware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 设置登录用户
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_authService"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, IEnumerable<IAuthService> _authService)
        {
            string token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                if (token.Contains('.'))
                {
                    await _authService.First(a => a.ServiceName == nameof(JwtAuthServiceImpl)).SetUserAsync(token);
                }
                else
                {
                    await _authService.First(a => a.ServiceName == nameof(OauthAuthServiceImpl)).SetUserAsync(token);
                }
            }
            await _next.Invoke(context);
            string GetToken()
            {
                string token = context.Request.Headers["token"];
                if (string.IsNullOrEmpty(token))
                {
                    token = context.Request.Query["token"];
                }
                return token == "null" ? null : token;
            }
        }
    }
}
