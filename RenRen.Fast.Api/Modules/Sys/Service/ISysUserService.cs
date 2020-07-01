using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysUserService
    {
        Task<PageUtils<SysUser>> QueryPage(SysUserParam pairs);

        /**
		 * 查询用户的所有菜单ID
		 */
        Task<List<long>> QueryAllMenuId(String userId);

        /**
		 * 根据用户名，查询系统用户
		 */
        Task<SysUser> QueryByUserNameAsync(String username);

        /**
		 * 保存用户
		 */
        Task SaveUser(SysUser user);

        /**
		 * 修改用户
		 */
        Task Update(SysUser user);

        /**
		 * 删除用户
		 */
        Task DeleteBatch(String[] userIds);

        /**
		 * 修改密码
		 * @param userId       用户ID
		 * @param password     原密码
		 * @param newPassword  新密码
		 */
        Task<bool> UpdatePassword(String userId, String password, String newPassword);

        Task<List<string>> QueryAllPerms(string userId);

        /**
         * 查询用户创建的角色ID列表
         */
        Task<List<long>> QueryRoleIdList(string createUserId);
    }
}
