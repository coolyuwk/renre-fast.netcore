using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class ScheduleJobLog
    {
        public long LogId { get; set; }
        public long JobId { get; set; }
        public string BeanName { get; set; }
        public string Params { get; set; }
        public byte Status { get; set; }
        public string Error { get; set; }
        public int Times { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
