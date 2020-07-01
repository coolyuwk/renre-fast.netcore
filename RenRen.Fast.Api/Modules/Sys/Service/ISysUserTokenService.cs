using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysUserTokenService
    {
        /**
		 * 生成token
		 * @param userId  用户ID
		 */
        Task<R> CreateToken(String userId);

        /**
		 * 退出，修改token值
		 * @param userId  用户ID
		 */
        Task Logout(String userId);
    }
}
