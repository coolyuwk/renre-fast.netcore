using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Extension;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysConfigServiceImpl : ISysConfigService
    {
        private readonly PassportDbContext _passportDbContext;

        public SysConfigServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        public async Task DeleteBatchAsync(long[] ids)
        {
            IQueryable<SysConfig> sysConfig = _passportDbContext.SysConfig.Where(s => ids.Contains(s.Id));
            _passportDbContext.SysConfig.RemoveRange(sysConfig);
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task<T> GetConfigObjectAsync<T>(string key)
        {
            string value = await GetValueAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new WinkSignException($"没有参数配置：{key}");
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<string> GetValueAsync(string key)
        {
            SysConfig config = await _passportDbContext.SysConfig.FirstOrDefaultAsync(c => c.ParamKey == key);
            return config?.ParamValue;
        }

        public async Task<PageUtils<SysConfig>> QueryPageAsync(SyConfigParam pairs)
        {
            IQueryable<SysConfig> query = _passportDbContext.SysConfig.Where(c => c.Status == 1);
            if (!string.IsNullOrEmpty(pairs.ParamKey))
            {
                query = query.Where(q => pairs.ParamKey.Contains(q.ParamKey));
            }
            PageUtils<SysConfig> page = new PageUtils<SysConfig>(pairs, query);
            return await Task.FromResult(page);
        }

        public async Task SaveConfigAsync(SysConfig config)
        {
            await _passportDbContext.SysConfig.AddAsync(config);
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SysConfig config)
        {
            SysConfig configEntity = await _passportDbContext.SysConfig.FindAsync(config.Id);
            config.CopyTo(configEntity);
            await _passportDbContext.SaveChangesAsync();
        }

        public async Task UpdateValueByKeyAsync(string key, string value)
        {
            List<SysConfig> list = _passportDbContext.SysConfig.Where(s => s.ParamKey == key).ToList();
            list.ForEach(l => l.ParamValue = value);
            await _passportDbContext.SaveChangesAsync();
        }
    }
}
