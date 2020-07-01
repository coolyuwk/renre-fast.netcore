using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
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
    public class SysUserServiceImpl : ISysUserService
    {
        private readonly PassportDbContext _passportDbContext;
        private readonly ISysUserRoleService _sysUserRoleService;

        public SysUserServiceImpl(PassportDbContext passportDbContext,
            ISysUserRoleService sysUserRoleService)
        {
            _passportDbContext = passportDbContext;
            _sysUserRoleService = sysUserRoleService;
        }

        public async Task DeleteBatch(string[] userIds)
        {
            await _passportDbContext.SysUser.Where(u => userIds.Contains(u.UserId)).BatchDeleteAsync();
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task<List<long>> QueryAllMenuId(string userId)
        {
            IQueryable<long> query = from ur in _passportDbContext.SysUserRole
                                     join _rm in _passportDbContext.SysRoleMenu on ur.RoleId equals _rm.RoleId into _rmg
                                     from rm in _rmg.DefaultIfEmpty()
                                     where ur.UserId == userId
                                     select rm.MenuId.Value;
            return await query.Distinct().ToListAsync();
        }

        /// <summary>
        /// 查询用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> QueryAllPerms(string userId)
        {
            IQueryable<string> query = (from ur in _passportDbContext.SysUserRole.Where(r => r.UserId == userId)
                                        join rm in _passportDbContext.SysRoleMenu on ur.RoleId equals rm.RoleId
                                        join m in _passportDbContext.SysMenu on rm.MenuId equals m.MenuId
                                        select m.Perms);
            return await query.ToListAsync();
        }

        public async Task<SysUser> QueryByUserNameAsync(string username)
        {
            return await _passportDbContext.SysUser.Where(s => s.Username == username).FirstOrDefaultAsync();
        }

        public async Task<PageUtils<SysUser>> QueryPage(SysUserParam pairs)
        {
            IQueryable<SysUser> query = _passportDbContext.SysUser.AsQueryable();
            if (!string.IsNullOrEmpty(pairs.CreateUserId))
            {
                query = query.Where(q => q.CreateUserId == pairs.CreateUserId);
            }

            if (!string.IsNullOrEmpty(pairs.UserName))
            {
                query = query.Where(q => pairs.UserName.Contains(q.Username));
            }
            PageUtils<SysUser> pageUtils = new PageUtils<SysUser>(pairs, query);
            return await Task.FromResult(pageUtils);
        }

        public Task<List<long>> QueryRoleIdList(string createUserId)
        {
            throw new NotImplementedException();
        }


        public async Task SaveUser(SysUser user)
        {
            user.UserId = StringUtils.NewId();
            user.CreateTime = DateTime.Now;
            //sha256加密
            string salt = RandomStringUtils.RandomAlphanumeric(20);
            user.Password = user.Password.HMACSHA256(salt);
            user.Salt = salt;
            _passportDbContext.SysUser.Add(user);
            _passportDbContext.SaveChanges();

            //检查角色是否越权
            await CheckRoleAsync(user);
            //保存用户与角色关系
            await _sysUserRoleService.SaveOrUpdate(user.UserId, user.RoleIdList);
        }

        public async Task Update(SysUser user)
        {
            SysUser userEntity = _passportDbContext.SysUser.Find(user.UserId);
            if (string.IsNullOrEmpty(user.Password))
            {
                user.Password = null;
            }
            else
            {
                user.Password = user.Password.HMACSHA256(userEntity.Salt);
            }

            _passportDbContext.SaveChanges();

            user.CopyTo(userEntity);
            //检查角色是否越权
            await CheckRoleAsync(user);

            //保存用户与角色关系
            await _sysUserRoleService.SaveOrUpdate(user.UserId, user.RoleIdList);
        }

        public async Task<bool> UpdatePassword(string userId, string password, string newPassword)
        {
            SysUser user = await _passportDbContext.SysUser.Where(u => u.UserId == userId && u.Password == password).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Password = newPassword;
                await _passportDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        /**
         * 检查角色是否越权
         */
        private async Task CheckRoleAsync(SysUser user)
        {
            if (user.RoleIdList == null || user.RoleIdList.Count == 0)
            {
                return;
            }
            //如果不是超级管理员，则需要判断用户的角色是否自己创建
            if (user.CreateUserId.Equals(Constant.SUPER_ADMIN, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            //查询用户创建的角色列表
            List<long> roleIdList = await QueryRoleIdList(user.CreateUserId);

            // 判断是否越权

            if (!roleIdList.Any(r => !user.RoleIdList.Contains(r)))
            {
                throw new WinkSignException("新增用户所选角色，不是本人创建");
            }
        }
    }
}
