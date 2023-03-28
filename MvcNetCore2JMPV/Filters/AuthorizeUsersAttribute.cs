using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcNetCore2JMPV.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;


            string controller =
               context.RouteData.Values["controller"].ToString();

            string action =
                context.RouteData.Values["action"].ToString();


            ITempDataProvider provider =
                context.HttpContext.RequestServices.GetService<ITempDataProvider>();

            var TempData = provider.LoadTempData(context.HttpContext);

            TempData["controller"] = controller;
            TempData["action"] = action;


            //ALMACENAMOS NUESTRO TEMPDATA DENTRO DE LA APP
            provider.SaveTempData(context.HttpContext, TempData);

            if (user.Identity.IsAuthenticated == false)
            {

                RouteValueDictionary rutalogin = new RouteValueDictionary(new
                {
                    controller = "Managed",
                    action = "Login"
                });
                RedirectToRouteResult result = new RedirectToRouteResult(rutalogin);
                context.Result = result;
            }

        }
    }
}
