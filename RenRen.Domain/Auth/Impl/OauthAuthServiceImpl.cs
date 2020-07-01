using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Auth.Entity.Model;
using RenRen.Domain.Common.Extension;
using RenRen.Domain.Common.Utils;
using RenRen.Domain.Config;

namespace RenRen.Domain.Auth.Impl
{
    /// <summary>
    /// 兼容renre-fast
    /// </summary>
    public class OauthAuthServiceImpl : IAuthService
    {
        private readonly AuthDbContext _passportDbContext;
        private readonly RenRenSettings _winkSignSettings;
        private readonly RoleMenuCollection _roleMenu;
        private readonly LoginUser _currentUser;
        public OauthAuthServiceImpl(AuthDbContext passportDbContext, IOptions<RenRenSettings> winkSignSettings, LoginUser currentUser, RoleMenuCollection roleMenu)
        {
            _passportDbContext = passportDbContext;
            _winkSignSettings = winkSignSettings.Value;
            _currentUser = currentUser;
            _roleMenu = roleMenu;
        }

        public string ServiceName => nameof(OauthAuthServiceImpl);

        public async Task SetUserAsync(string token)
        {
            if (string.IsNullOrEmpty(_currentUser.UserId))
            {
                var user = await (from t in _passportDbContext.SysUserTokens.AsNoTracking()
                                  join u in _passportDbContext.Users.AsNoTracking()
                                  on t.UserId equals u.UserId
                                  where t.Token == token
                                  select new LoginUser
                                  {
                                      UserId = u.UserId,
                                      Mobile = u.Mobile,
                                      UserName = u.Username,
                                      Salt = u.Salt
                                  })
                            .FirstOrDefaultAsync();
                user?.CopyTo(_currentUser);
            }
        }

        public async Task<bool> PermissionAsync(string token, string path)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(token))
            {
                return true;
            }

            //Token过期
            var tokenEntity = await _passportDbContext.SysUserTokens.AsNoTracking().Where(s => s.Token == token).FirstOrDefaultAsync();
            if (tokenEntity == null)
            {
                return false;
            }
            //if (tokenEntity.ExpireTime != null && tokenEntity.ExpireTime <= DateTime.Now)
            //{
            //    return false;
            //}

            //超级管理员
            if (tokenEntity.UserId == Constant.SUPER_ADMIN)
            {
                await SetUserAsync(token);
                return true;
            }

            //路由权限 当前用户角色是否拥有菜单权限
            var roles = _passportDbContext.SysUserRoles.AsNoTracking().Where(s => s.UserId == tokenEntity.UserId).Select(s => s.RoleId.Value).ToList();
            foreach (var role in roles)
            {
                if (!_roleMenu.TryGetValue(role, out List<string> _menu))
                {
                    var perms = (from rm in _passportDbContext.SysRoleMenus.AsNoTracking()
                                 join menu in _passportDbContext.SysMenus.AsNoTracking()
                                 on rm.MenuId equals menu.MenuId
                                 where rm.RoleId == role && menu.Perms != null
                                 select menu.Perms).ToList();

                    _menu = perms.SelectMany(perm => perm.Split(',')).ToList();
                    _roleMenu.TryAdd(role, _menu);
                }
                return _menu.Any(m => m.Equals(path, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }
    }

    public class RoleMenuCollection : ConcurrentDictionary<long, List<string>>
    {

    }
}
