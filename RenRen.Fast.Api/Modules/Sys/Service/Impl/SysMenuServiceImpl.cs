using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysMenuServiceImpl : ISysMenuService
    {
        private readonly PassportDbContext _passportDbContext;
        private readonly ISysUserService _sysUserService;

        public SysMenuServiceImpl(PassportDbContext passportDbContext, ISysUserService sysUserService)
        {
            _passportDbContext = passportDbContext;
            _sysUserService = sysUserService;
        }

        public async Task Delete(long menuId)
        {
            var sysMenu = await _passportDbContext.SysMenu.FindAsync(menuId);
            _passportDbContext.SysMenu.Remove(sysMenu);
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task<List<SysMenu>> GetUserMenuListAsync(string userId)
        {
            //系统管理员，拥有最高权限
            if (userId.Equals(Constant.SUPER_ADMIN))
            {
                return await GetAllMenuListAsync(null);
            }

            //用户菜单列表
            List<long> menuIdList = await _sysUserService.QueryAllMenuId(userId);
            return await GetAllMenuListAsync(menuIdList);
        }

        public async Task<List<SysMenu>> QueryListParentId(long parentId, List<long> menuIdList)
        {
            List<SysMenu> menuList = await QueryListParentId(parentId);
            if (menuIdList == null)
            {
                return menuList;
            }

            List<SysMenu> userMenuList = new List<SysMenu>();
            foreach (var menu in menuList)
            {
                if (menuIdList.Contains(menu.MenuId))
                {
                    userMenuList.Add(menu);
                }
            }
            return userMenuList;
        }

        public async Task<List<SysMenu>> QueryListParentId(long parentId)
        {
            // select * from sys_menu where parent_id = #{parentId} order by order_num asc
            return await _passportDbContext.SysMenu.Where(s => s.ParentId == parentId).OrderBy(s => s.OrderNum).ToListAsync();
        }

        public async Task<List<SysMenu>> QueryNotButtonList()
        {
            // select * from sys_menu where type != 2 order by order_num asc
            return await _passportDbContext.SysMenu.Where(s => s.Type != 2).OrderBy(s => s.OrderNum).ToListAsync();
        }

        /**
         * 获取所有菜单列表
         */
        private async Task<List<SysMenu>> GetAllMenuListAsync(List<long> menuIdList)
        {
            //查询根菜单列表
            List<SysMenu> menuList = await QueryListParentId(0L, menuIdList);
            //递归获取子菜单
            await GetMenuTreeListAsync(menuList, menuIdList);

            return menuList;
        }

        /**
         * 递归
         */
        private async Task<List<SysMenu>> GetMenuTreeListAsync(List<SysMenu> menuList, List<long> menuIdList)
        {
            List<SysMenu> subMenuList = new List<SysMenu>();

            foreach (var entity in menuList)
            {
                //目录
                if (entity.Type == (int)Constant.MenuType.目录)
                {
                    entity.list = await GetMenuTreeListAsync(await QueryListParentId(entity.MenuId, menuIdList), menuIdList);
                }
                subMenuList.Add(entity);
            }
            return subMenuList;
        }
    }
}
