using System.Web;
using System.Web.Mvc;

namespace EuroSpaceCenter.util {
    public class CustomAuthorizeAttribute : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            if (!httpContext.User.Identity.IsAuthenticated) {
                // no user authenticated
                return false;
            }
            // authenticated user
            string username = httpContext.User.Identity.Name;

            Provider prov = new Provider();

            return prov.IsUserInRole(username, this.Roles);
        }
    }
}