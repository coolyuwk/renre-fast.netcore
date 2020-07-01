using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Oss.Param;

namespace RenRen.Fast.Api.Modules.Oss.Service
{
    public interface ISysOssService
    {
        Task<PageUtils<SysOss>> QueryPageAsync(OssListParam pairs);

        Task<SysOss> SaveAsync(SysOss sysOss);

        Task RemoveByIds(long[] ids);
    }
}
