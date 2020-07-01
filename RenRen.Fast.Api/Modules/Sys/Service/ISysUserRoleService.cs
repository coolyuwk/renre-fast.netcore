using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysUserRoleService
    {
		Task SaveOrUpdate(string userId, List<long> roleIdList);

		/**
		 * 根据用户ID，获取角色ID列表
		 */
		Task<List<long>> QueryRoleIdList(string userId);

		/**
		 * 根据角色ID数组，批量删除
		 */
		Task<int> DeleteBatch(long[] roleIds);
	}
}
