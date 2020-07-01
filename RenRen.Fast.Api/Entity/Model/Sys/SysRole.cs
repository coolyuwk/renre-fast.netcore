using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysRole
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }

        [NotMapped]
        public List<long> MenuIdList { get; set; }
    }

    public class SysRoleValidator : AbstractValidator<SysRole>
    {
        public SysRoleValidator()
        {
            RuleFor(x => x.RoleName).NotNull().WithMessage("角色名称不能为空");
        }
    }
}
