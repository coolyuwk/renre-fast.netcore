namespace RenRen.Domain.Config
{
    public partial class RenRenSettings
    {
        /// <summary>
        /// 全局请求前缀
        /// </summary>
        public string ContextPath { get; set; }
        /// <summary>
        /// Redis缓存
        /// </summary>
        public RedisSettings Redis { get; set; }
        /// <summary>
        /// JWT
        /// </summary>
        public JwtSettings Jwt { get; set; }

        /// <summary>
        /// 接口白名单
        /// </summary>
        public string[] WhiteList { get; set; }

        /// <summary>
        /// 腾讯云短信配置
        /// </summary>
        public TencentSmsSettings TencentSms { get; set; }
        /// <summary>
        /// 阿里云存储配置
        /// </summary>
        public AliyunOssSettings AliyunOss { get; set; }

        public class RedisSettings
        {
            public string ConnectionString { get; set; }
        }

        public class JwtSettings
        {
            public string Secret { get; set; }
            public long Expire { get; set; }
        }

        public class TencentSmsSettings
        {
            public string SecretId { get; set; }
            public string SecretKey { get; set; }
            public string AppId { get; set; }
        }

        public class AliyunOssSettings
        {
            /// <summary>
            /// 阿里云绑定的域名
            /// </summary>
            public string AliyunDomain { get; set; }
            /// <summary>
            /// 阿里云路径前缀
            /// </summary>
            public string AliyunPrefix { get; set; }
            /// <summary>
            /// 阿里云EndPoint
            /// </summary>
            public string AliyunEndPoint { get; set; }
            /// <summary>
            /// 阿里云AccessKeyId
            /// </summary>
            public string AliyunAccessKeyId { get; set; }
            /// <summary>
            /// 阿里云AccessKeySecret
            /// </summary>
            public string AliyunAccessKeySecret { get; set; }
            /// <summary>
            /// 阿里云BucketName
            /// </summary>
            public string AliyunBucketName { get; set; }
        }
    }
}
