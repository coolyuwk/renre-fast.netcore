using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class ScheduleJob
    {
        public long JobId { get; set; }
        public string BeanName { get; set; }
        public string Params { get; set; }
        public string CronExpression { get; set; }
        public byte? Status { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
