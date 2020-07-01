using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_user_role")]
    public class AuthSysUserRole
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
    }
}
