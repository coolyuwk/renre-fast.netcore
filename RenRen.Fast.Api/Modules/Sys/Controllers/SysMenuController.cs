using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RenRen.Domain.Common;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Extension;
using RenRen.Domain.Common.Utils;
using RenRen.Domain.Auth.Entity;
using Microsoft.AspNetCore.Authorization;

namespace RenRen.Fast.Api.Modules.Sys.Controllers
{
    [Route("sys/menu")]
    [ApiController]
    public class SysMenuController : AbstractController
    {
        private readonly ISysMenuService _sysMenuService;
        private readonly IShiroService _shiroService;
        private readonly PassportDbContext _passportDbContext;
        private readonly LoginUser _loginUser;

        public SysMenuController(ISysMenuService sysMenuService, IShiroService shiroService, PassportDbContext passportDbContext, LoginUser loginUser)
        {
            _sysMenuService = sysMenuService;
            _shiroService = shiroService;
            _passportDbContext = passportDbContext;
            _loginUser = loginUser;
        }

        /// <summary>
        /// 导航菜单
        /// </summary>
        [HttpGet("nav")]
        public async Task<R> NavAsync()
        {
            var menuList = await _sysMenuService.GetUserMenuListAsync(_loginUser.UserId);
            var permissions = await _shiroService.GetUserPermissionsAsync(_loginUser.UserId);
            return R.Ok().Put("menuList", menuList).Put("permissions", permissions);
        }

        /// <summary>
        /// 所有菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:list")]
        public async Task<List<SysMenu>> ListAsync()
        {
            List<SysMenu> menuList = await _passportDbContext.SysMenu.ToListAsync();
            menuList.ForEach(sysMenuEntity =>
            {
                SysMenu parentMenuEntity = _passportDbContext.SysMenu.Find(sysMenuEntity.ParentId);
                if (parentMenuEntity != null)
                {
                    sysMenuEntity.ParentName = parentMenuEntity.Name;
                }
            });
            return menuList;
        }

        /// <summary>
        /// 选择菜单(添加、修改菜单)
        /// </summary>
        [HttpGet("select")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:select")]
        public async Task<R> SelectAsync()
        {
            //查询列表数据
            List<SysMenu> menuList = await _sysMenuService.QueryNotButtonList();
            //添加顶级菜单
            SysMenu root = new SysMenu()
            {
                MenuId = 0L,
                Name = "一级菜单",
                ParentId = -1L,
                open = true
            };
            menuList.Add(root);
            return R.Ok().Put("menuList", menuList);
        }


        /// <summary>
        /// 菜单信息
        /// </summary>
        [HttpGet("info/{menuId}")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:info")]
        public async Task<R> InfoAsync([FromRoute] long menuId)
        {
            SysMenu menu = await _passportDbContext.SysMenu.FindAsync(menuId);
            return R.Ok().Put("menu", menu);
        }


        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost("save")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:save")]
        public async Task<R> SaveAsync([FromBody] SysMenu menu)
        {
            VerifyForm(menu);
            _passportDbContext.SysMenu.Add(menu);
            await _passportDbContext.SaveChangesAsync();
            return R.Ok();
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPost("update")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:update")]
        public async Task<R> UpdateAsync([FromBody] SysMenu menu)
        {
            VerifyForm(menu);
            SysMenu menuEntity = await _passportDbContext.SysMenu.FindAsync(menu.MenuId);
            menu.CopyTo(menuEntity);
            await _passportDbContext.SaveChangesAsync();
            return R.Ok();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        [HttpPost("delete/{menuId}")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:menu:delete")]
        public async Task<R> DeleteAsync([FromRoute] long menuId)
        {
            if (menuId <= 30)
            {
                return R.Error("系统菜单，不能删除");
            }

            //判断是否有子菜单或按钮
            List<SysMenu> menuList = await _sysMenuService.QueryListParentId(menuId);
            if (menuList.Count > 0)
            {
                return R.Error("请先删除子菜单或按钮");
            }
            await _sysMenuService.Delete(menuId);

            return R.Ok();
        }

        /// <summary>
        /// 验证参数是否正确
        /// </summary>
        /// <param name="menu"></param>
        private void VerifyForm(SysMenu menu)
        {
            if (string.IsNullOrEmpty(menu.Name))
            {
                throw new WinkSignException("菜单名称不能为空");
            }

            if (menu.ParentId == null)
            {
                throw new WinkSignException("上级菜单不能为空");
            }

            //菜单
            if (menu.Type == (int)Constant.MenuType.菜单)
            {
                if (string.IsNullOrEmpty(menu.Url))
                {
                    throw new WinkSignException("菜单URL不能为空");
                }
            }

            //上级菜单类型
            int parentType = (int)Constant.MenuType.目录;
            if (menu.ParentId != 0)
            {
                SysMenu parentMenu = _passportDbContext.SysMenu.Find(menu.ParentId);
                parentType = parentMenu.Type.Value;
            }

            //目录、菜单
            if (menu.Type == (int)Constant.MenuType.目录 ||
                    menu.Type == (int)Constant.MenuType.菜单)
            {
                if (parentType != (int)Constant.MenuType.目录)
                {
                    throw new WinkSignException("上级菜单只能为目录类型");
                }
                return;
            }

            //按钮
            if (menu.Type == (int)Constant.MenuType.按钮)
            {
                if (parentType != (int)Constant.MenuType.菜单)
                {
                    throw new WinkSignException("上级菜单只能为菜单类型");
                }
                return;
            }
        }
    }
}
