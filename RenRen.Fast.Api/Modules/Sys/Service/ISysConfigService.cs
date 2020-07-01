using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public interface ISysConfigService
    {
        Task<PageUtils<SysConfig>> QueryPageAsync(SyConfigParam pairs);

        /**
         * 保存配置信息
         */
        Task SaveConfigAsync(SysConfig config);

        /**
         * 更新配置信息
         */
        Task UpdateAsync(SysConfig config);

        /**
         * 根据key，更新value
         */
        Task UpdateValueByKeyAsync(string key, string value);

        /**
         * 删除配置信息
         */
        Task DeleteBatchAsync(long[] ids);

        /**
         * 根据key，获取配置的value值
         * 
         * @param key           key
         */
        Task<string> GetValueAsync(string key);

        /**
         * 根据key，获取value的Object对象
         * @param key    key
         */
        Task<T> GetConfigObjectAsync<T>(string key);
    }
}
