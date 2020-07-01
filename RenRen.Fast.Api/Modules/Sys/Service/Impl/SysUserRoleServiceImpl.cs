using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysUserRoleServiceImpl : ISysUserRoleService
    {
        private readonly PassportDbContext _passportDbContext;

        public SysUserRoleServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        public async Task<int> DeleteBatch(long[] roleIds)
        {
            await _passportDbContext.SysUserRole.Where(r => roleIds.Contains(r.RoleId.Value)).BatchDeleteAsync();
            return await _passportDbContext.SaveChangesAsync();
        }

        public async Task<List<long>> QueryRoleIdList(string userId)
        {
            return await _passportDbContext.SysUserRole.Where(r => r.UserId == userId).Select(r => r.RoleId.Value).ToListAsync();
        }

        public async Task SaveOrUpdate(string userId, List<long> roleIdList)
        {
            //先删除用户与角色关系
            await _passportDbContext.SysUserRole.Where(u => u.UserId == userId).BatchDeleteAsync();

            if (roleIdList != null && roleIdList.Count > 0)
            {
                //保存用户与角色关系
                var roles = roleIdList.Select(roleId => new SysUserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                }).ToList();
                _passportDbContext.SysUserRole.AddRange(roles);
            }
            await _passportDbContext.SaveChangesAsync();
        }
    }
}
