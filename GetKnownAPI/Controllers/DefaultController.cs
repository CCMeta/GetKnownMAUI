using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetKnownAPI.Controllers
{
    public abstract class DefaultController : ControllerBase
    {
        protected readonly int _uid;

        protected DefaultController(IHttpContextAccessor context)
        {
            if (context.HttpContext is null) throw new ArgumentNullException("context.HttpContext is null");
            if (context.HttpContext.Items.TryGetValue("uid", out var uid))
            {
                _uid = (int)uid;
                return;
            }
        }

    }
}
