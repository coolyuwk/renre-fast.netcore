using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Auth.Entity.Model;
using RenRen.Domain.Common.Utils;
using RenRen.Domain.Config;

namespace RenRen.Domain.Auth.Impl
{
    public class JwtAuthServiceImpl : IAuthService
    {
        private readonly RenRenSettings _winkSignSettings;
        private readonly LoginUser _currentUser;

        public JwtAuthServiceImpl(IOptions<RenRenSettings> winkSignSettings, LoginUser currentUser)
        {
            _winkSignSettings = winkSignSettings.Value;
            _currentUser = currentUser;
        }

        public string ServiceName => nameof(JwtAuthServiceImpl);

        public Task SetUserAsync(string token)
        {
            var payload = JwtUtils.GetPayload(token);
            _currentUser.UserId = payload.sub;
            return Task.CompletedTask;
        }

        public Task<bool> PermissionAsync(string token, string path)
        {
            return Task.FromResult(JwtUtils.CheckToken(token, _winkSignSettings.Jwt.Secret));
        }
    }
}
