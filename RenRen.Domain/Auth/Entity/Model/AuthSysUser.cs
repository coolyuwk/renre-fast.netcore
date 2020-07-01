using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_user")]
    public class AuthSysUser
    {
        [Key]
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("salt")]
        public string Salt { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("mobile")]
        public string Mobile { get; set; }
        [Column("status")]
        public byte? Status { get; set; }
        [Column("create_user_id")]
        public string CreateUserId { get; set; }
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
    }
}
