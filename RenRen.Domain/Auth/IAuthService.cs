using System.Threading.Tasks;
using RenRen.Domain.Auth.Entity;

namespace RenRen.Domain.Auth
{
    public interface IAuthService
    {
        string ServiceName { get; }
        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="token"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> PermissionAsync(string token, string path);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetUserAsync(string token);
    }
}
