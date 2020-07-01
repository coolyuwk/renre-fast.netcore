using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Sms.V20190711;
using TencentCloud.Sms.V20190711.Models;
using RenRen.Domain.Config;

namespace RenRen.Fast.Api.Modules.Msg.Cloud
{
    public class TencentSmsServiceImpl : ICloudSmsService
    {
        private readonly RenRenSettings _winkSignSettings;

        public TencentSmsServiceImpl(IOptions<RenRenSettings> winkSignSettings)
        {
            _winkSignSettings = winkSignSettings.Value;
        }

        public async Task<bool> SendAsync(List<string> mobile, string content)
        {
            Credential cred = new Credential
            {
                SecretId = _winkSignSettings.TencentSms.SecretId,
                SecretKey = _winkSignSettings.TencentSms.SecretKey
            };
            SmsClient client = new SmsClient(cred, "ap-guangzhou");
            SendSmsRequest req = new SendSmsRequest
            {
                SmsSdkAppid = _winkSignSettings.TencentSms.AppId,
                /* 短信签名内容: 使用 UTF-8 编码，必须填写已审核通过的签名，可登录 [短信控制台] 查看签名信息 */
                Sign = "",
                PhoneNumberSet = mobile.Select(m => $"+86{m}").ToArray(),
                TemplateID = "317355",
                /* 模板参数: 若无模板参数，则设置为空*/
                TemplateParamSet = new string[] { content, "1" }
            };
            SendSmsResponse resp = await client.SendSms(req);
            return true;
        }
    }
}
