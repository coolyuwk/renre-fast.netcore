using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using NETCore.Encrypt.Extensions;
using RenRen.Fast.Api.Entity;
using RenRen.Fast.Api.Modules.App.Form;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Aspect.Middlerware;

namespace RenRen.Fast.Api.Modules.App.Service.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly PassportDbContext _passportDbContext;
        public UserServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        public async Task<string> LoginAsync(LoginForm form)
        {
            User user = await QueryByMobileAsync(form.Mobile);
            if (user == null || user.Password != form.Password.SHA256())
            {
                throw new WinkSignException("手机号或密码错误");
            }

            return user.Id;
        }

        public async Task<User> QueryByMobileAsync(string mobile)
        {
            return await _passportDbContext.Users.Where(u => u.Mobile == mobile).FirstOrDefaultAsync();
        }


        public async Task<User> RegisterAsync(UserDto form)
        {
            if (_passportDbContext.Users.Any(u => u.Mobile == form.Mobile))
            {
                throw new WinkSignException("已存在相同的手机号");
            }
            User user = TinyMapper.Map<User>(form);

            user.Password = user.Password.SHA256();
            if (string.IsNullOrEmpty(user.UserName))
            {
                user.UserName = user.Mobile;
            }

            await _passportDbContext.Users.AddAsync(user);
            await _passportDbContext.SaveChangesAsync();

            user.Password = null;
            return user;
        }

        public async Task<bool> ChangePwdAsync(UserDto form)
        {
            User user = await _passportDbContext.Users.Where(u => u.Mobile == form.Mobile).SingleOrDefaultAsync();
            user.Password = form.Password.SHA256();
            await _passportDbContext.SaveChangesAsync();
            return true;
        }
    }
}
