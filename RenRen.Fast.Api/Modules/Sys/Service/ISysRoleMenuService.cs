using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysRoleMenuService
    {
		Task SaveOrUpdate(long roleId, List<long> menuIdList);

		/**
		 * 根据角色ID，获取菜单ID列表
		 */
		Task<List<long>> QueryMenuIdList(long roleId);

		/**
		 * 根据角色ID数组，批量删除
		 */
		Task<int> DeleteBatch(long[] roleIds);
	}
}
