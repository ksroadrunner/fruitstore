using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using FruitStore.WebApp.Extensions;
using FruitStore.DataAccess.Services;

namespace FruitStore.WebApp.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.Any(a => a is AllowAnonymousAttribute))
                return;

            try
            {
                // It's just an another demo project :-p
                // if (user == null)
                // {
                //     var cookie = context.HttpContext.Request.Cookies["ss"];
                //     if (cookie != null)
                //     {
                //     }
                // }

                var user = context.HttpContext.Session.Get<Entity.User>();
                if (user == null)
                {
                    context.Result = new RedirectResult("/Security/Login");
                }
            }
            catch { }

            base.OnActionExecuting(context);
        }
    }
}