using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysMenu
    {
        public SysMenu() { }
        public SysMenu(long menuId, long? parentId, string name, string url, string perms, int? type, string icon, int? orderNum)
        {
            MenuId = menuId;
            ParentId = parentId;
            Name = name;
            Url = url;
            Perms = perms;
            Type = type;
            Icon = icon;
            OrderNum = orderNum;
        }

        public long MenuId { get; set; }
        public long? ParentId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Perms { get; set; }
        public int? Type { get; set; }
        public string Icon { get; set; }
        public int? OrderNum { get; set; }


        [NotMapped]
        public string ParentName { get; set; }

        [NotMapped]
        public Boolean open { get; set; }

        [NotMapped]
        public List<SysMenu> list { get; set; }
    }
}
