using Microsoft.AspNetCore.Mvc;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Sys.Param;
using RenRen.Fast.Api.Modules.Sys.Service;
using System.Threading.Tasks;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common;

namespace RenRen.Fast.Api.Modules.Sys.Controllers
{
    [Route("sys/config")]
    [ApiController]
    public class SysConfigController : AbstractController
    {
        private readonly ISysConfigService _sysConfigService;
        private readonly PassportDbContext _passportDbContext;
        public SysConfigController(ISysConfigService sysConfigService, PassportDbContext passportDbContext = null)
        {
            _sysConfigService = sysConfigService;
            _passportDbContext = passportDbContext;
        }
        /// <summary>
        /// 所有配置列表
        /// </summary>
        [HttpGet("list")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:config:list")]
        public async Task<R> ListAsync([FromQuery] SyConfigParam pairs)
        {
            PageUtils<SysConfig> page = await _sysConfigService.QueryPageAsync(pairs);
            return R.Ok().Put("page", page);
        }


        /// <summary>
        /// 配置信息
        /// </summary>
        [HttpGet("info/{id}")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:config:info")]
        public async Task<R> InfoAsync([FromRoute] long id)
        {
            ValueTask<SysConfig> config = _passportDbContext.SysConfig.FindAsync(id);
            return R.Ok().Put("config", await config);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        [HttpPost("save")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:config:save")]
        public async Task<R> SaveAsync([FromBody] SysConfig config)
        {
            await _sysConfigService.SaveConfigAsync(config);
            return R.Ok();
        }

        /// <summary>
        /// 修改配置
        /// </summary>
        [HttpPost("update")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:config:update")]
        public async Task<R> UpdateAsync([FromBody] SysConfig config)
        {
            await _sysConfigService.UpdateAsync(config);
            return R.Ok();
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        [HttpPost("delete")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:config:delete")]
        public async Task<R> DeleteAsync([FromBody] long[] ids)
        {
            await _sysConfigService.DeleteBatchAsync(ids);
            return R.Ok();
        }
    }
}
