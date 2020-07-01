using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RenRen.Domain.Common.Utils
{
    public static class JwtUtils
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string CreateToken(IEnumerable<Claim> claims, string securityKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                //expires: DateTime.Now.AddMinutes(settings.ExpMinutes),
                signingCredentials: creds);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }


        /// <summary>
        /// 生成Jwt
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GenerateToken(string userId, string securityKey)
        {
            //声明claim
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Typ,"JWT"),
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMonths(2).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), //过期时间
            };
            return CreateToken(claims, securityKey);
        }


        ///// <summary>
        ///// 刷新token
        ///// </summary>
        ///// <returns></returns>
        //public static string RefreshToken(string oldToken)
        //{
        //    var pl = GetPayload(oldToken);
        //    //声明claim
        //    var claims = new Claim[] {
        //        new Claim(JwtRegisteredClaimNames.Sub, pl?.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, pl?.UserId),
        //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixDate().ToString(), ClaimValueTypes.Integer64),//签发时间
        //        new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixDate().ToString(), ClaimValueTypes.Integer64),//生效时间
        //        new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(settings.ExpMinutes).ToUnixDate().ToString(), ClaimValueTypes.Integer64), //过期时间
        //        new Claim(JwtRegisteredClaimNames.Iss, settings.Issuer),
        //        new Claim(JwtRegisteredClaimNames.Aud, settings.Audience),
        //        new Claim(ClaimTypes.Name, pl?.UserName),
        //        new Claim(ClaimTypes.Role, pl?.RoleId),
        //        new Claim(ClaimTypes.Sid, pl?.UserId)
        //    };

        //    return IsExp(oldToken) ? CreateToken(claims) : null;
        //}


        /// <summary>
        /// 从token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadJwtToken(token);
            return securityToken?.Claims;
        }


        /// <summary>
        /// 从Token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <param name="securityKey">securityKey明文,Java加密使用的是Base64</param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string token, string securityKey)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    ValidateLifetime = false
                };
                return handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static bool CheckToken(string token, string securityKey)
        {
            var principal = GetPrincipal(token, securityKey);
            if (principal is null)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取Token中的载荷数据
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static JwtPayload GetPayload(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = jwtHandler.ReadJwtToken(token);
            return new JwtPayload
            {
                sub = securityToken.Payload[JwtRegisteredClaimNames.Sub]?.ToString(),
                exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(securityToken.Payload[JwtRegisteredClaimNames.Exp].ToString())).ToLocalTime().DateTime,
                iat = securityToken.Payload[JwtRegisteredClaimNames.Iat]?.ToString()
            };
        }


        /// <summary>
        /// 获取Token中的载荷数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static T GetPayload<T>(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            return JsonConvert.DeserializeObject<T>(jwtToken.Payload.SerializeToJson());
        }


        /// <summary>
        /// 判断token是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsExp(string token)
        {
            return false;
            //return  GetPrincipal(token)?.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value?.TimeStampToDate() < DateTime.Now;
            //return GetPayload(token).ExpTime < DateTime.Now;
        }
    }

    /// <summary>
    /// Jwt载荷信息
    /// </summary>
    public class JwtPayload
    {
        public string sub { get; set; }

        public string iat { get; set; }

        public DateTime exp { get; set; }
    }
}
