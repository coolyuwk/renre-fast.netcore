using System.Collections.Generic;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysRoleService
    {
        Task<PageUtils<SysRole>> QueryPage(SysRoleParam pairs);

        Task SaveRole(SysRole role);

        Task Update(SysRole role);

        Task DeleteBatch(long[] roleIds);



    }
}
