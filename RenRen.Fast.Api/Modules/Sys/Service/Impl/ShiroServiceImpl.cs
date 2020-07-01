using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class ShiroServiceImpl : IShiroService
    {
        private readonly PassportDbContext _passportDbContext;
        private readonly ISysUserService _sysUserService;
        private readonly ISysMenuService _sysMenuService;

        public ShiroServiceImpl(ISysMenuService sysMenuService, PassportDbContext passportDbContext, ISysUserService sysUserService)
        {
            _sysMenuService = sysMenuService;
            _passportDbContext = passportDbContext;
            _sysUserService = sysUserService;
        }

        public async Task<List<string>> GetUserPermissionsAsync(string userId)
        {
            List<string> permsList = new List<string>();

            //系统管理员，拥有最高权限
            if (userId.Equals(Constant.SUPER_ADMIN))
            {
                List<SysMenu> menuList = await _passportDbContext.SysMenu.ToListAsync();
                permsList = menuList.Select(m => m.Perms).ToList();
            }
            else
            {
                permsList = await _sysUserService.QueryAllPerms(userId);
            }
            //用户权限列表
            var permsSet = new List<string>();
            permsList?.ForEach(perms =>
            {
                if (!string.IsNullOrEmpty(perms))
                {
                    permsSet.AddRange(perms.Trim().Split(","));
                }
            });
            return permsSet;
        }

        public SysUserToken QueryByToken(string token)
        {
            throw new NotImplementedException();
        }

        public SysUser QueryUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
