using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Oss.Cloud
{
    public class CloudStorageConfig
    {
        /// <summary>
        /// 类型 1：七牛  2：阿里云  3：腾讯云
        /// </summary>
        public int type;

        /// <summary>
        /// 七牛绑定的域名
        /// </summary>
        public string qiniuDomain;
        /// <summary>
        /// 七牛路径前缀
        /// </summary>
        public string qiniuPrefix;
        /// <summary>
        /// 七牛ACCESS_KEY
        /// </summary>
        public string qiniuAccessKey;
        /// <summary>
        /// 七牛SECRET_KEY
        /// </summary>
        public string qiniuSecretKey;
        /// <summary>
        /// 七牛存储空间名
        /// </summary>
        public string qiniuBucketName;

        /// <summary>
        /// 阿里云绑定的域名
        /// </summary>
        public string aliyunDomain;
        /// <summary>
        /// 阿里云路径前缀
        /// </summary>
        public string aliyunPrefix;
        /// <summary>
        /// 阿里云EndPoint
        /// </summary>
        public string aliyunEndPoint;
        /// <summary>
        /// 阿里云AccessKeyId
        /// </summary>
        public string aliyunAccessKeyId;
        /// <summary>
        /// 阿里云AccessKeySecret
        /// </summary>
        public string aliyunAccessKeySecret;
        /// <summary>
        /// 阿里云BucketName
        /// </summary>
        public string aliyunBucketName;

        /// <summary>
        /// 腾讯云绑定的域名
        /// </summary>
        public string qcloudDomain;
        /// <summary>
        /// 腾讯云路径前缀
        /// </summary>
        public string qcloudPrefix;
        /// <summary>
        /// 腾讯云AppId
        /// </summary>
        public int qcloudAppId;
        /// <summary>
        /// 腾讯云SecretId
        /// </summary>
        public string qcloudSecretId;
        /// <summary>
        /// 腾讯云SecretKey
        /// </summary>
        public string qcloudSecretKey;
        /// <summary>
        /// 腾讯云BucketName
        /// </summary>
        public string qcloudBucketName;
        /// <summary>
        /// 腾讯云COS所属地区
        /// </summary>
        public string qcloudRegion;
    }
}
