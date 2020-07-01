using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Sys.Param
{
    public class SysUserParam : IPage
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public string CreateUserId { get; set; }

        public string UserName { get; set; }
    }
}
