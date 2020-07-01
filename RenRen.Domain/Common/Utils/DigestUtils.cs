using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RenRen.Domain.Common.Utils
{
    [Obsolete]
    public class DigestUtils
    {
        /// <summary>
        /// SHA256 转换为 Hex字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sha256Hex(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Hex.ByteArrayToHexString(hash);
        }

        /// <summary>
        /// SHA256 转换为 Hex字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Sha256Hex(string data, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Hex.ByteArrayToHexString(hash);
        }

        /// <summary>
        /// SHA256 转换为 Hex字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Sha256Hex(byte[] bytes)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Hex.ByteArrayToHexString(hash);
        }

    }

    public class Hex
    {
        /// <summary>
        /// 字节数组转换为Hex字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="toLowerCase"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data, bool toLowerCase = true)
        {
            var hex = BitConverter.ToString(data).Replace("-", string.Empty);
            return toLowerCase ? hex.ToLower() : hex.ToUpper();
        }
    }
}
