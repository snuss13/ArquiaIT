using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FG.Models
{
    public class FGAuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (httpContext.Session["sessionStore"] != null && (httpContext.Session["sessionStore"] as FG.Models.SessionStore).Usuario.Rol == "Administrador");
        }
    }


    public class FGAuthorizeAdministratorOrColaboradorAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (httpContext.Session["sessionStore"] != null);
        }
    }

}