using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Sys.Param
{
    public class SyConfigParam : IPage
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string ParamKey { get; set; }
    }
}
