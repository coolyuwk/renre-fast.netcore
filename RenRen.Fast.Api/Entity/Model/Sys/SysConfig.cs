using FluentValidation;
using System;
using System.Collections.Generic;

namespace RenRen.Fast.Api.Entity
{
    public partial class SysConfig
    {
        public long Id { get; set; }
        public string ParamKey { get; set; }
        public string ParamValue { get; set; }
        public byte? Status { get; set; }
        public string Remark { get; set; }
    }

    public class SysConfigValidator : AbstractValidator<SysConfig>
    {
        public SysConfigValidator()
        {
            RuleFor(x => x.ParamKey).NotNull().WithMessage("参数名不能为空");
            RuleFor(x => x.ParamValue).NotNull().WithMessage("参数值不能为空");
        }
    }
}
