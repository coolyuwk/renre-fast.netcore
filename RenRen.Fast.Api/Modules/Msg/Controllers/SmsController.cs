using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Modules.Msg.Form;
using RenRen.Fast.Api.Modules.Msg.Service;

namespace RenRen.Fast.Api.Modules.Msg.Controllers
{
    /// <summary>
    /// 短信
    /// </summary>
    [Route("sms")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        [HttpPost("send")]
        public async Task<R> SendAsync([FromBody] SmsDto sms)
        {
            await _smsService.SendAsync(sms);
            return R.Ok();
        }

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        [HttpPost("verification")]
        public async Task<R> Verification([FromBody] SmsDto sms)
        {
            var result = await _smsService.VerificationAsync(sms.MsgType, sms.Content, sms.Mobile);
            return R.Ok().Put(result);
        }
    }
}
