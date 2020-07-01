using Microsoft.EntityFrameworkCore;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Extension;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysRoleServiceImpl : ISysRoleService
    {
        private readonly ISysUserService _sysUserService;
        private readonly PassportDbContext _passportDbContext;
        private readonly ISysRoleMenuService _sysRoleMenuService;
        private readonly ISysUserRoleService _sysUserRoleService;
        public SysRoleServiceImpl(ISysUserService sysUserService, PassportDbContext passportDbContext, ISysRoleMenuService sysRoleMenuService, ISysUserRoleService sysUserRoleService)
        {
            _sysUserService = sysUserService;
            _passportDbContext = passportDbContext;
            _sysRoleMenuService = sysRoleMenuService;
            _sysUserRoleService = sysUserRoleService;
        }



        public async Task DeleteBatch(long[] roleIds)
        {
            //删除角色
            List<SysRole> roles = await _passportDbContext.SysRole.Where(r => roleIds.Contains(r.RoleId)).ToListAsync();
            _passportDbContext.SysRole.RemoveRange(roles);

            //删除角色与菜单关联
            await _sysRoleMenuService.DeleteBatch(roleIds);

            //删除角色与用户关联
            await _sysUserRoleService.DeleteBatch(roleIds);

            await _passportDbContext.SaveChangesAsync();
        }

        public async Task<PageUtils<SysRole>> QueryPage(SysRoleParam pairs)
        {
            IQueryable<SysRole> query = _passportDbContext.SysRole.AsQueryable();
            if (!string.IsNullOrEmpty(pairs.CreateUserId))
            {
                query = query.Where(q => q.CreateUserId == pairs.CreateUserId);
            }
            if (!string.IsNullOrEmpty(pairs.RoleName))
            {
                query = query.Where(q => q.RoleName == pairs.RoleName);
            }
            PageUtils<SysRole> page = new PageUtils<SysRole>(pairs, query);
            return await Task.FromResult(page);
        }

        public Task<List<long>> QueryRoleIdList(string createUserId)
        {
            return _passportDbContext.SysRole.Where(r => r.CreateUserId == createUserId).Select(r => r.RoleId).ToListAsync();
        }

        public async Task SaveRole(SysRole role)
        {
            //检查权限是否越权
            await CheckPremsAsync(role);
            role.CreateTime = DateTime.Now;
            await _passportDbContext.SysRole.AddAsync(role);
            await _passportDbContext.SaveChangesAsync();

            //保存角色与菜单关系
            await _sysRoleMenuService.SaveOrUpdate(role.RoleId, role.MenuIdList);
        }

        public async Task Update(SysRole role)
        {
            SysRole roleEntity = await _passportDbContext.SysRole.FindAsync(role.RoleId);
            role.CopyTo(roleEntity);

            //检查权限是否越权
            await CheckPremsAsync(role);
            //更新角色与菜单关系
            await _sysRoleMenuService.SaveOrUpdate(role.RoleId, role.MenuIdList);
        }

        /// <summary>
        /// 检查权限是否越权
        /// </summary>
        /// <param name="role"></param>
        private async Task CheckPremsAsync(SysRole role)
        {
            //如果不是超级管理员，则需要判断角色的权限是否超过自己的权限
            if (role.CreateUserId.Equals(Domain.Common.Utils.Constant.SUPER_ADMIN, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            //查询用户所拥有的菜单列表
            List<long> menuIdList = await _sysUserService.QueryAllMenuId(role.CreateUserId);

            //判断是否越权
            if (menuIdList.Any(m => !role.MenuIdList.Contains(m)))
            {
                throw new WinkSignException("新增角色的权限，已超出你的权限范围");
            }
        }
    }
}
