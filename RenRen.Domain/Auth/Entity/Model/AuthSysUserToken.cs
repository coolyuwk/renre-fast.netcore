using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_user_token")]
    public partial class AuthSysUserToken
    {
        [Key]
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("token")]
        public string Token { get; set; }
        [Column("expire_time")]
        public DateTime? ExpireTime { get; set; }
        [Column("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
