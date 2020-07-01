using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_role")]
    public class AuthSysRole
    {
        [Key]
        [Column("role_id")]
        public long RoleId { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        [Column("create_user_id")]
        public string CreateUserId { get; set; }
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
    }
}
