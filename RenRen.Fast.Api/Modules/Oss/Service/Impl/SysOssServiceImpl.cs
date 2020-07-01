using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Oss.Param;

namespace RenRen.Fast.Api.Modules.Oss.Service.Impl
{
    public class SysOssServiceImpl : ISysOssService
    {
        private readonly PassportDbContext _passportDbContext;

        public SysOssServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        public async Task<PageUtils<SysOss>> QueryPageAsync(OssListParam pairs)
        {
            PageUtils<SysOss> page = new PageUtils<SysOss>(pairs, _passportDbContext.SysOss.AsQueryable());
            return await Task.FromResult(page);
        }

        public async Task RemoveByIds(long[] ids)
        {
            await _passportDbContext.SysOss.Where(s => ids.Contains(s.Id)).BatchDeleteAsync();
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task<SysOss> SaveAsync(SysOss sysOss)
        {
            await _passportDbContext.SysOss.AddAsync(sysOss);
            await _passportDbContext.SaveChangesAsync();
            return sysOss;
        }
    }
}
