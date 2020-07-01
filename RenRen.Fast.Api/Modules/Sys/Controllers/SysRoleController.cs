using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;
using RenRen.Fast.Api.Modules.Sys.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Common;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Sys.Controllers
{
    [Route("sys/role")]
    [ApiController]
    public class SysRoleController : AbstractController
    {
        private readonly ISysRoleService _sysRoleService;
        private readonly PassportDbContext _passportDbContext;
        private readonly ISysRoleMenuService _sysRoleMenuService;
        private readonly LoginUser _loginUser;
        public SysRoleController(ISysRoleService sysRoleService, PassportDbContext passportDbContext = null, ISysRoleMenuService sysRoleMenuService = null, LoginUser loginUser = null)
        {
            _sysRoleService = sysRoleService;
            _passportDbContext = passportDbContext;
            _sysRoleMenuService = sysRoleMenuService;
            _loginUser = loginUser;
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        [HttpGet("list")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:list")]
        public async Task<R> ListAsync([FromQuery] SysRoleParam pairs)
        {
            //如果不是超级管理员，则只查询自己创建的角色列表
            if (!_loginUser.UserId.Equals(Constant.SUPER_ADMIN, StringComparison.OrdinalIgnoreCase))
            {
                pairs.CreateUserId = _loginUser.UserId;
            }

            var page = await _sysRoleService.QueryPage(pairs);

            return R.Ok().Put("page", page);
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        [HttpGet("select")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:select")]
        public async Task<R> SelectAsync()
        {
            IQueryable<SysRole> query = _passportDbContext.SysRole.AsQueryable();
            //如果不是超级管理员，则只查询自己所拥有的角色列表
            if (!_loginUser.UserId.Equals(Constant.SUPER_ADMIN, StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(r => r.CreateUserId == _loginUser.UserId);
            }
            List<SysRole> list = await query.ToListAsync();

            return R.Ok().Put("list", list);
        }

        /// <summary>
        /// 角色信息
        /// </summary>
        [HttpGet("info/{roleId}")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:info")]
        public async Task<R> InfoAsync([FromRoute] long roleId)
        {
            SysRole role = await _passportDbContext.SysRole.FindAsync(roleId);
            //查询角色对应的菜单
            List<long> menuIdList = await _sysRoleMenuService.QueryMenuIdList(roleId);
            role.MenuIdList = menuIdList;
            return R.Ok().Put("role", role);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [HttpPost("save")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:save")]
        public async Task<R> Save([FromBody] SysRole role)
        {
            role.CreateUserId = _loginUser.UserId;
            await _sysRoleService.SaveRole(role);
            return R.Ok();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        [HttpPost("update")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:update")]
        public async Task<R> Update([FromBody] SysRole role)
        {
            role.CreateUserId = _loginUser.UserId;

            await _sysRoleService.Update(role);
            return R.Ok();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [HttpPost("delete")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:role:delete")]
        public async Task<R> DeleteAsync([FromBody] long[] roleIds)
        {
            await _sysRoleService.DeleteBatch(roleIds);
            return R.Ok();
        }
    }
}
