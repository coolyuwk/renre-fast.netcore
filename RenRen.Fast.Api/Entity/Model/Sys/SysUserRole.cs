using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysUserRole
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long? RoleId { get; set; }
    }
}
