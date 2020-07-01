using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RenRen.Domain.Common.Aspect.Middlerware;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Common.Utils;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.Oss.Cloud;
using RenRen.Fast.Api.Modules.Oss.Param;
using RenRen.Fast.Api.Modules.Oss.Service;
using RenRen.Fast.Api.Modules.Sys.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Oss.Controllers
{
    [Route("sys/oss")]
    [ApiController]
    public class SysOssController : ControllerBase
    {
        private readonly OSSFactory _oSSFactory;
        private readonly ISysOssService _sysOssService;
        private readonly ISysConfigService _sysConfigService;
        public SysOssController(OSSFactory oSSFactory, ISysOssService sysOssService, ISysConfigService sysConfigService)
        {
            _oSSFactory = oSSFactory;
            _sysOssService = sysOssService;
            _sysConfigService = sysConfigService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:oss:all")]
        public async Task<R> ListAsync([FromQuery] OssListParam pairs)
        {
            PageUtils<SysOss> page = await _sysOssService.QueryPageAsync(pairs);
            return R.Ok().Put("page", page);
        }

        /// <summary>
        /// 云存储配置信息
        /// </summary>
        [HttpGet("config")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:oss:all")]
        public async Task<R> ConfigAsync()
        {
            CloudStorageConfig config = await _sysConfigService.GetConfigObjectAsync<CloudStorageConfig>(ConfigConstant.CLOUD_STORAGE_CONFIG_KEY);
            return R.Ok().Put("config", config);
        }


        /// <summary>
        /// 保存云存储配置信息
        /// </summary>
        [HttpPost("saveConfig")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:oss:all")]
        public async Task<R> SaveConfigAsync([FromBody] CloudStorageConfig config)
        {
            Constant.CloudService cloud = (Constant.CloudService)Enum.ToObject(typeof(Constant.CloudService), config.type);
            switch (cloud)
            {
                case Constant.CloudService.七牛云:

                    break;
                case Constant.CloudService.阿里云:

                    break;
                case Constant.CloudService.腾讯云:

                    break;
                default:
                    return R.Error("云存储类型错误");
            }

            await _sysConfigService.UpdateValueByKeyAsync(ConfigConstant.CLOUD_STORAGE_CONFIG_KEY, JsonConvert.SerializeObject(config));
            return R.Ok();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        [HttpPost("upload")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:oss:all")]
        public async Task<R> UploadAsync(List<IFormFile> file)
        {
            CloudStorageService ossClient = await _oSSFactory.BuildAsync();
            foreach (IFormFile formFile in file)
            {
                if (formFile.Length > 0)
                {
                    using Stream stream = formFile.OpenReadStream();
                    string url = ossClient.Upload(stream, $"{Guid.NewGuid():N}{ formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'))}");
                    //保存文件信息
                    SysOss sysOss = new SysOss()
                    {
                        Url = url,
                        CreateDate = DateTime.Now
                    };
                    await _sysOssService.SaveAsync(sysOss);
                }
            }
            return R.Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost("delete")]
        [RequiresPermissions(ClaimType.Oauth2, "sys:oss:all")]
        public async Task<R> DeleteAsync([FromBody] long[] ids)
        {
            await _sysOssService.RemoveByIds(ids);
            return R.Ok();
        }
    }
}
