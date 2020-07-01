using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysLog
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Operation { get; set; }
        public string Method { get; set; }
        public string Params { get; set; }
        public long Time { get; set; }
        public string Ip { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
