using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_role_menu")]
    public class AuthSysRoleMenu
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
        [Column("menu_id")]
        public long? MenuId { get; set; }
    }
}
