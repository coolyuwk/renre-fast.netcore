using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class QrtzJobDetails
    {
        public QrtzJobDetails()
        {
            QrtzTriggers = new HashSet<QrtzTriggers>();
        }

        public string SchedName { get; set; }
        public string JobName { get; set; }
        public string JobGroup { get; set; }
        public string Description { get; set; }
        public string JobClassName { get; set; }
        public string IsDurable { get; set; }
        public string IsNonconcurrent { get; set; }
        public string IsUpdateData { get; set; }
        public string RequestsRecovery { get; set; }
        public byte[] JobData { get; set; }

        public virtual ICollection<QrtzTriggers> QrtzTriggers { get; set; }
    }
}
