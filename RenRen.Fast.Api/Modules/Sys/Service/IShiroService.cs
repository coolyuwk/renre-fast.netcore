using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface IShiroService
    {
        /**
         * 获取用户权限列表
         */
        Task<List<string>> GetUserPermissionsAsync(string userId);

        SysUserToken QueryByToken(string token);

        /**
         * 根据用户ID，查询用户
         * @param userId
         */
        SysUser QueryUser(string userId);
    }
}
