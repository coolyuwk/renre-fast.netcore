using Microsoft.AspNetCore.Mvc;
using RenRen.Domain.Common.Aspect.Middlerware;

namespace RenRen.Domain.Common
{
    [RequiresPermissions(ClaimType.Oauth2)]
    public class AbstractController : ControllerBase
    {

    }
}
