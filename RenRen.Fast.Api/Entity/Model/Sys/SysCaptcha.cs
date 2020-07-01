using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysCaptcha
    {
        public string Uuid { get; set; }
        public string Code { get; set; }
        public DateTime? ExpireTime { get; set; }
    }
}
