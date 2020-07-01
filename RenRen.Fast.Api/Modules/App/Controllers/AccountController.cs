using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RenRen.Domain.Common.Utils;
using RenRen.Domain.Config;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.App.Form;
using RenRen.Fast.Api.Modules.App.Service;
using RenRen.Fast.Api.Modules.Msg.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.App.Controllers
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [Route("app")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISmsService _smsService;
        private readonly PassportDbContext _passportDbContext;
        private readonly IUserService _userService;
        private readonly RenRenSettings _winkSignSettings;
        public AccountController(IUserService userService,
            IOptions<RenRenSettings> winkSignSettings, ISmsService smsService, PassportDbContext passportDbContext)
        {
            _winkSignSettings = winkSignSettings.Value;
            _userService = userService;
            _smsService = smsService;
            _passportDbContext = passportDbContext;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<R> LoginAsync([FromBody] LoginForm form)
        {
            //用户登录
            string userId = await _userService.LoginAsync(form);

            //生成token
            string token = JwtUtils.GenerateToken(userId, _winkSignSettings.Jwt.Secret);

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                { "token", token },
                { "expire", _winkSignSettings.Jwt.Expire }
            };
            return R.Ok().Put(map);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<R> RegisterAsync([FromBody] UserDto form)
        {
            //表单校验
            bool result = await _smsService.VerificationAsync(Entity.Msg.MsgType.注册, form.Code, form.Mobile);
            if (!result)
            {
                return R.Error(400, "验证码错误");
            }

            User user = await _userService.RegisterAsync(form);
            return R.Ok().Put(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("changePwd")]
        public async Task<R> UpdatePwdAsync([FromBody] UserDto form)
        {
            //表单校验
            bool result = await _smsService.VerificationAsync(Entity.Msg.MsgType.修改密码, form.Code, form.Mobile);
            if (!result)
            {
                return R.Error(400, "验证码错误");
            }

            bool user = await _userService.ChangePwdAsync(form);
            return R.Ok().Put(user);
        }
    }
}
