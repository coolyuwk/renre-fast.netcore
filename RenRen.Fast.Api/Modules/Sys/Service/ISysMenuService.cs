using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysMenuService
    {
        /**
	     * 根据父菜单，查询子菜单
	     * @param parentId 父菜单ID
	     * @param menuIdList  用户菜单ID
	     */
        Task<List<SysMenu>> QueryListParentId(long parentId, List<long> menuIdList);

        /**
		 * 根据父菜单，查询子菜单
		 * @param parentId 父菜单ID
		 */
        Task<List<SysMenu>> QueryListParentId(long parentId);

        /**
		 * 获取不包含按钮的菜单列表
		 */
        Task<List<SysMenu>> QueryNotButtonList();

        /**
		 * 获取用户菜单列表
		 */
        Task<List<SysMenu>> GetUserMenuListAsync(String userId);

        /**
		 * 删除
		 */
        Task Delete(long menuId);
    }
}
