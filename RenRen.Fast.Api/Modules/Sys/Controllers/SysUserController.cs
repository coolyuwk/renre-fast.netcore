using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Form;
using RenRen.Fast.Api.Modules.Sys.Param;
using RenRen.Fast.Api.Modules.Sys.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Common;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Utils;
using static RenRen.Domain.Common.Utils.Constant;

namespace RenRen.Fast.Api.Modules.Sys.Controllers
{
    [Route("sys/user")]
    [ApiController]
    public class SysUserController : AbstractController
    {
        private readonly ISysUserService _sysUserService;
        private readonly PassportDbContext _passportDbContext;
        private readonly LoginUser _loginUser;

        public SysUserController(ISysUserService sysUserService, PassportDbContext passportDbContext, LoginUser loginUser)
        {
            _sysUserService = sysUserService;
            _passportDbContext = passportDbContext;
            _loginUser = loginUser;
        }

        /// <summary>
        /// 所有用户列表
        /// </summary>
        [HttpGet("list")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:user:list")]
        public async Task<R> ListAsync([FromQuery] SysUserParam pairs)
        {
            //只有超级管理员，才能查看所有管理员列表
            if (!_loginUser.UserId.Equals(SUPER_ADMIN, StringComparison.OrdinalIgnoreCase))
            {

                pairs.CreateUserId = _loginUser.UserId;
            }

            var page = await _sysUserService.QueryPage(pairs);

            return R.Ok().Put("page", page);
        }

        /// <summary>
        /// 获取登录的用户信息
        /// </summary>
        [HttpGet("info")]
        public async Task<R> InfoAsync()
        {
            var user = await _passportDbContext.SysUser.FindAsync(_loginUser.UserId);
            return R.Ok().Put("user", user);
        }

        /// <summary>
        /// 修改登录用户密码
        /// </summary>
        [HttpPost("password")]
        public async Task<R> PasswordAsync([FromBody] PasswordForm form)
        {
            //sha256加密
            string password = form.password.HMACSHA256(_loginUser.Salt);
            //sha256加密
            string newPassword = form.newPassword.HMACSHA256(_loginUser.Salt);

            //更新密码
            var flag = await _sysUserService.UpdatePassword(_loginUser.UserId, password, newPassword);
            if (!flag)
            {
                return R.Error("原密码不正确");
            }
            return R.Ok();
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        [HttpGet("info/{userId}")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:user:info")]
        public async Task<R> InfoAsync([FromRoute] string userId)
        {
            SysUser user = await _passportDbContext.SysUser.FindAsync(userId);
            //获取用户所属的角色列表
            user.RoleIdList = await _sysUserService.QueryRoleIdList(userId);
            return R.Ok().Put("user", user);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        [HttpPost("save")]
        [RuleSetForClientSideMessages(ValidatorGroup.Add)]
        [RequiresPermissions(ClaimType.Oauth2, "sys:user:save")]
        public async Task<R> SaveAsync([FromBody, CustomizeValidator(RuleSet = ValidatorGroup.Add)] SysUser user)
        {
            user.CreateUserId = _loginUser.UserId;
            await _sysUserService.SaveUser(user);
            return R.Ok();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        [HttpPost("update")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:user:update")]
        public async Task<R> UpdateAsync([FromBody, CustomizeValidator(RuleSet = ValidatorGroup.Update)] SysUser user)
        {
            user.CreateUserId = _loginUser.UserId;
            await _sysUserService.Update(user);
            return R.Ok();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        [HttpPost("delete")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:user:delete")]
        public async Task<R> DeleteAsync([FromBody] string[] userIds)
        {
            if (userIds.Contains(SUPER_ADMIN))
            {
                return R.Error("系统管理员不能删除");
            }

            if (userIds.Contains(_loginUser.UserId))
            {
                return R.Error("当前用户不能删除");
            }
            await _sysUserService.DeleteBatch(userIds);
            return R.Ok();
        }
    }
}
