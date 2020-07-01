using Aliyun.OSS;
using System.IO;

namespace RenRen.Fast.Api.Modules.Oss.Cloud
{
    public class AliyunCloudStorageService : CloudStorageService
    {
        private readonly OssClient _ossClient;
        private readonly CloudStorageConfig _cloudStorageConfig;
        public AliyunCloudStorageService(CloudStorageConfig cloudStorageConfig)
        {
            _cloudStorageConfig = cloudStorageConfig;
            _ossClient = new OssClient(cloudStorageConfig.aliyunEndPoint, cloudStorageConfig.aliyunAccessKeyId, cloudStorageConfig.aliyunAccessKeySecret);
        }

        public override string Upload(byte[] data, string path)
        {
            return Upload(data, path);
        }

        public override string Upload(Stream inputStream, string path)
        {
            _ossClient.PutObject(_cloudStorageConfig.aliyunBucketName, path, inputStream);
            return $"{_cloudStorageConfig.aliyunDomain}/{path}";
        }

        public override string UploadSuffix(byte[] data, string suffix)
        {
            return Upload(data, GetPath(_cloudStorageConfig.aliyunPrefix, suffix));
        }

        public override string UploadSuffix(Stream inputStream, string suffix)
        {
            return Upload(inputStream, GetPath(_cloudStorageConfig.aliyunPrefix, suffix));
        }
    }
}
