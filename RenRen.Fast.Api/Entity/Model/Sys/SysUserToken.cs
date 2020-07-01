using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysUserToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
