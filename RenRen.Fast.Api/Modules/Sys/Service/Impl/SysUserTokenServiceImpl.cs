using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;
using RenRen.Fast.Api.Entity;

namespace RenRen.Fast.Api.Modules.Sys.Service.Impl
{
    public class SysUserTokenServiceImpl : ISysUserTokenService
    {
        private readonly PassportDbContext _passportDbContext;
        //12小时后过期
        private static readonly int EXPIRE = 12;
        public SysUserTokenServiceImpl(PassportDbContext passportDbContext)
        {
            _passportDbContext = passportDbContext;
        }

        private string GenerateValue()
        {
            return Base64UrlEncoder.Encode(Guid.NewGuid().ToString());
        }
        public async Task<R> CreateToken(string userId)
        {
            //生成一个token
            string token = GenerateValue();

            //当前时间
            DateTime now = DateTime.Now;
            //过期时间
            DateTime expireTime = now.AddHours(EXPIRE);

            //判断是否生成过token
            SysUserToken tokenEntity = await _passportDbContext.SysUserToken.FindAsync(userId);
            if (tokenEntity == null)
            {
                tokenEntity = new SysUserToken
                {
                    UserId = userId,
                    Token = token,
                    UpdateTime = now,
                    ExpireTime = expireTime
                };

                //保存token
                await _passportDbContext.SysUserToken.AddAsync(tokenEntity);
            }
            else
            {
                tokenEntity.Token = token;
                tokenEntity.UpdateTime = now;
                tokenEntity.ExpireTime = expireTime;

                //更新token
                _passportDbContext.SysUserToken.Update(tokenEntity);
            }
            await _passportDbContext.SaveChangesAsync();
            R r = R.Ok().Put("token", token).Put("expire", EXPIRE);
            return r;
        }

        public async Task Logout(string userId)
        {
            //生成一个token
            var token = GenerateValue();

            //修改token
            var sysUserTokenEntity = await _passportDbContext.SysUserToken.FindAsync(userId);
            sysUserTokenEntity.Token = token;
            await _passportDbContext.SaveChangesAsync();
        }
    }
}
