using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class QrtzSimpleTriggers
    {
        public string SchedName { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public long RepeatCount { get; set; }
        public long RepeatInterval { get; set; }
        public long TimesTriggered { get; set; }

        public virtual QrtzTriggers QrtzTriggers { get; set; }
    }
}
