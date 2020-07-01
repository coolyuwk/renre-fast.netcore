using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using RenRen.Fast.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysRoleMenuServiceImpl : ISysRoleMenuService
    {
        private readonly PassportDbContext _passportDbContext;

        public SysRoleMenuServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        public async Task<int> DeleteBatch(long[] roleIds)
        {
            if (roleIds == null)
            {
                return 0;
            }
            await _passportDbContext.SysRoleMenu.Where(r => roleIds.Contains(r.RoleId.Value)).BatchDeleteAsync();
            int count = await _passportDbContext.SaveChangesAsync();
            return count;
        }

        public async Task<List<long>> QueryMenuIdList(long roleId)
        {
            return await _passportDbContext.SysRoleMenu.Where(r => r.RoleId == roleId).Select(r => r.MenuId.Value).ToListAsync();
        }

        public async Task SaveOrUpdate(long roleId, List<long> menuIdList)
        {
            //先删除角色与菜单关系
            await DeleteBatch(new long[] { roleId });

            if (menuIdList != null && menuIdList.Count > 0)
            {
                //保存角色与菜单关系
                await _passportDbContext.SysRoleMenu.AddRangeAsync(menuIdList.Select(menuId => new SysRoleMenu()
                {
                    MenuId = menuId,
                    RoleId = roleId
                }).ToList());
            }
            await _passportDbContext.SaveChangesAsync();
        }
    }
}
