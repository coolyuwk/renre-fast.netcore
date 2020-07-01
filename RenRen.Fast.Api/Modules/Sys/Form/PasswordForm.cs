using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RenRen.Fast.Api.Modules.Sys.Form
{
    public class PasswordForm
    {
        /**
         * 原密码
         */
        public String password;
        /**
         * 新密码
         */
        [Required(ErrorMessage = "新密码不为能空")]
        public String newPassword;
    }
}
