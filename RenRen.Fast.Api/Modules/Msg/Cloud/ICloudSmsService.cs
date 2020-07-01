using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Msg.Cloud
{
    public interface ICloudSmsService
    {
        Task<bool> SendAsync(List<string> mobile, string content);
    }
}
