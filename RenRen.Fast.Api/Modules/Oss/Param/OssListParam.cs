using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Domain.Common.Utils;

namespace RenRen.Fast.Api.Modules.Oss.Param
{
    public class OssListParam : IPage
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}