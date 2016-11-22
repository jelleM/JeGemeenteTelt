using System.Linq;
using System.Web.Mvc;

namespace BEP.BL.Models.Gebruikers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // The user is not authenticated
                //in het geval dat het kapoet is !!!!
                //base.HandleUnauthorizedRequest(filterContext);

                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccesDenied.cshtml"
                };
            }
            else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {
                // The user is not in any of the listed roles => 
                // show the unauthorized view
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/AccesDenied.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
