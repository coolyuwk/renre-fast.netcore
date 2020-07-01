using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using RenRen.Fast.Api.Modules.Sys.Form;
using RenRen.Fast.Api.Modules.Sys.Service;
using System;
using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;
using RenRen.Domain.Common;
using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Sys.Controllers
{
    [Route("sys")]
    [ApiController]
    public class SysLoginController : AbstractController
    {
        private readonly ISysCaptchaService sysCaptchaService;
        private readonly ISysUserService _sysUserService;
        private readonly ISysUserTokenService _sysUserTokenService;
        private readonly LoginUser _loginUser;
        public SysLoginController(ISysCaptchaService sysCaptchaService, ISysUserService sysUserService, ISysUserTokenService sysUserTokenService, LoginUser loginUser)
        {
            this.sysCaptchaService = sysCaptchaService;
            _sysUserService = sysUserService;
            _sysUserTokenService = sysUserTokenService;
            _loginUser = loginUser;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("~/captcha.jpg")]
        public async Task<IActionResult> CaptchaAsync(string uuid)
        {
            byte[] captch = await sysCaptchaService.GetCaptchaAsync(uuid);
            return File(captch, "image/jpeg");
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<R> Login([FromBody] SysLoginForm form)
        {
            bool captcha = await sysCaptchaService.ValidateAsync(form.uuid, form.captcha);
            if (!captcha)
            {
                return R.Error("验证码不正确");
            }

            //用户信息
            Entity.SysUser user = await _sysUserService.QueryByUserNameAsync(form.username);

            //账号不存在、密码错误
            //TODO: 需要验证加密是否正确

            if (user == null || !user.Password.Equals(EncryptProvider.HMACSHA256(form.password, user.Salt), StringComparison.OrdinalIgnoreCase))
            {
                return R.Error("账号或密码不正确");
            }

            //账号锁定
            if (user.Status == 0)
            {
                return R.Error("账号已被锁定,请联系管理员");
            }

            //生成token，并保存到数据库
            R r = await _sysUserTokenService.CreateToken(user.UserId);
            return r;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("/sys/logout")]
        public async Task<R> LogoutAsync()
        {
            await _sysUserTokenService.Logout(_loginUser.UserId);
            return R.Ok();
        }
    }
}
