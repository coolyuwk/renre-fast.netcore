﻿using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class QrtzTriggers
    {
        public string SchedName { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string JobName { get; set; }
        public string JobGroup { get; set; }
        public string Description { get; set; }
        public long? NextFireTime { get; set; }
        public long? PrevFireTime { get; set; }
        public int? Priority { get; set; }
        public string TriggerState { get; set; }
        public string TriggerType { get; set; }
        public long StartTime { get; set; }
        public long? EndTime { get; set; }
        public string CalendarName { get; set; }
        public short? MisfireInstr { get; set; }
        public byte[] JobData { get; set; }

        public virtual QrtzJobDetails QrtzJobDetails { get; set; }
        public virtual QrtzCronTriggers QrtzCronTriggers { get; set; }
        public virtual QrtzSimpleTriggers QrtzSimpleTriggers { get; set; }
        public virtual QrtzSimpropTriggers QrtzSimpropTriggers { get; set; }
    }
}
