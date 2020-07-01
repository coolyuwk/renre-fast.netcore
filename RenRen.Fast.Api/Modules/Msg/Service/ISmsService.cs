using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity.Msg;
using RenRen.Fast.Api.Modules.Msg.Form;

namespace RenRen.Fast.Api.Modules.Msg.Service
{
    public interface ISmsService
    {
        Task<bool> SendAsync(SmsDto msgSms);
        Task<bool> VerificationAsync(MsgType msgType, string content, string mobile);
    }
}
