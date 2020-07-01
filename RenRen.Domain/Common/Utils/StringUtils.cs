using System;
using System.Collections.Generic;
using System.Text;

namespace RenRen.Domain.Common.Utils
{
    public class StringUtils
    {
        /// <summary>
        /// 创建ID
        /// </summary>
        /// <returns></returns>
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
