using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Service
{
    public interface ISysCaptchaService
    {
        /**
         * 获取图片验证码
         */
        Task<byte[]> GetCaptchaAsync(string uuid);

        /**
         * 验证码效验
         * @param uuid  uuid
         * @param code  验证码
         * @return  true：成功  false：失败
         */
        Task<bool> ValidateAsync(string uuid, string code);
    }
}
