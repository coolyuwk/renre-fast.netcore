using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Entity.Msg;
using RenRen.Fast.Api.Modules.Msg.Cloud;
using RenRen.Fast.Api.Modules.Msg.Form;
using RenRen.Domain.Common.Aspect.Middlerware;

namespace RenRen.Fast.Api.Modules.Msg.Service.Impl
{
    public class SmsCaptchaServiceImpl : ISmsService
    {
        private readonly PassportDbContext _passportDbContext;
        private readonly int Count = 8;
        private readonly int EffectiveTime = 120;
        private readonly ICloudSmsService _cloudSmsService;
        public SmsCaptchaServiceImpl(PassportDbContext passportDbContext, ICloudSmsService cloudSmsService)
        {
            _passportDbContext = passportDbContext;
            _cloudSmsService = cloudSmsService;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="msgSms"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(SmsDto msgSms)
        {
            var count = _passportDbContext.MsgSms
                .Where(s => msgSms.MsgType == s.MsgType
                && DateTime.Now.Date < s.SendTime
                && s.Mobile == msgSms.Mobile)
                .OrderByDescending(s => s.CreateTime)
                .Count();

            if (count >= Count)
            {
                throw new WinkSignException("当天发送短信超出限制");
            }
            string content = new Random().Next(100000, 999999).ToString();
            await _cloudSmsService.SendAsync(new List<string>() { msgSms.Mobile }, content);

            var entity = TinyMapper.Map<MsgSms>(msgSms);
            entity.ExpiredTime = entity.SendTime.AddSeconds(EffectiveTime);
            entity.Content = content;
            await _passportDbContext.MsgSms.AddAsync(entity);
            await _passportDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 检验
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="content"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task<bool> VerificationAsync(MsgType msgType, string content, string mobile)
        {
            var extime = DateTime.Now;
            var sms = await _passportDbContext.MsgSms.Where(s => msgType == s.MsgType
            && s.Content == content
            && s.ExpiredTime >= extime
            && s.Mobile == mobile)
                .FirstOrDefaultAsync();

            if (sms == null)
            {
                return false;
            }

            sms.Deleted = true;
            await _passportDbContext.SaveChangesAsync();
            return true;
        }
    }
}
