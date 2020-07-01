using System;
using System.IO;
using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Oss.Cloud
{
    public abstract class CloudStorageService
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="prefix">prefix 前缀</param>
        /// <param name="suffix">suffix 后缀</param>
        /// <returns>返回上传路径</returns>
        public string GetPath(string prefix, string suffix)
        {
            //生成uuid
            string uuid = StringUtils.NewId();
            //文件路径
            string path = $"{DateTime.Now:yyyyMMdd}/{uuid}";

            if (!string.IsNullOrWhiteSpace(prefix))
            {
                path = prefix + "/" + path;
            }

            return path + suffix;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="data">文件字节数组</param>
        /// <param name="path">文件路径，包含文件名</param>
        /// <returns>返回http地址</returns>
        public abstract string Upload(byte[] data, string path);

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="data">文件字节数组</param>
        /// <param name="suffix">后缀</param>
        /// <returns>返回http地址</returns>
        public abstract string UploadSuffix(byte[] data, string suffix);

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="inputStream">字节流</param>
        /// <param name="path">文件路径，包含文件名</param>
        /// <returns> 返回http地址</returns>
        public abstract string Upload(Stream inputStream, string path);

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="inputStream">字节流</param>
        /// <param name="suffix">后缀</param>
        /// <returns>返回http地址</returns>
        public abstract string UploadSuffix(Stream inputStream, string suffix);
    }
}
