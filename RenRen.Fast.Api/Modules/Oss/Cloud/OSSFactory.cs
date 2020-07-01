using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Modules.Sys.Service;
using System.Threading.Tasks;
using RenRen.Domain.Common.Aspect.Middlerware;

namespace RenRen.Fast.Api.Modules.Oss.Cloud
{
    public class OSSFactory
    {
        public static readonly string CLOUD_STORAGE_CONFIG_KEY = "CLOUD_STORAGE_CONFIG_KEY";
        private readonly ISysConfigService _sysConfigService;

        public OSSFactory(ISysConfigService sysConfigService)
        {
            _sysConfigService = sysConfigService;
        }

        public async Task<CloudStorageService> BuildAsync()
        {
            //获取云存储配置信息
            CloudStorageConfig config = await _sysConfigService.GetConfigObjectAsync<CloudStorageConfig>(CLOUD_STORAGE_CONFIG_KEY);

            return config.type switch
            {
                (int)Constant.CloudService.七牛云 => throw new WinkSignException("没有实现"),
                (int)Constant.CloudService.阿里云 => new AliyunCloudStorageService(config),
                (int)Constant.CloudService.腾讯云 => throw new WinkSignException("没有实现"),
                _ => null,
            };
        }
    }
}
