using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static RenRen.Domain.Common.Utils.Constant;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte? Status { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        [NotMapped]
        public List<long> RoleIdList { get; set; }
    }

    public class SysUserValidator : AbstractValidator<SysUser>
    {
        public SysUserValidator()
        {
            RuleSet(ValidatorGroup.Add, () =>
            {
                RuleFor(x => x.Username).NotNull().WithMessage("用户名不能为空");
                RuleFor(x => x.Password).NotNull().WithMessage("密码不能为空");
            });

            RuleSet(ValidatorGroup.Update, () =>
            {
                RuleFor(x => x.Password).NotNull().WithMessage("密码不能为空");
            });
        }
    }
}
