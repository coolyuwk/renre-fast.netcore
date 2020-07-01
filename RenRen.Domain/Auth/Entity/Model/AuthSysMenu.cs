using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Domain.Auth.Entity.Model
{
    [Table("sys_menu")]
    public class AuthSysMenu
    {
        [Key]
        [Column("menu_id")]
        public long MenuId { get; set; }
        [Column("parent_id")]
        public long? ParentId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("perms")]
        public string Perms { get; set; }
        [Column("type")]
        public int? Type { get; set; }
        [Column("icon")]
        public string Icon { get; set; }
        [Column("order_num")]
        public int? OrderNum { get; set; }
    }
}
