using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Modules.App.Form;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.App.Service
{
    public interface IUserService
    {
        Task<User> QueryByMobileAsync(String mobile);


        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        Task<string> LoginAsync(LoginForm form);

        Task<User> RegisterAsync(UserDto form);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        Task<bool> ChangePwdAsync(UserDto form);
    }
}
