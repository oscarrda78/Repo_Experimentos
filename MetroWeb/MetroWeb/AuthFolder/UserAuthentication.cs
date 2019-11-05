using MetroWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace MetroWeb.AuthFolder
{
    public class UserAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        private BDGestionIntegradaEntitiesContext db = new BDGestionIntegradaEntitiesContext();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
           AplicationUser.Usuario = Convert.ToString(filterContext.HttpContext.Session["Usuario"]);
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["Usuario"]))) 
            {

                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
               filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Cuenta" },
                    { "action", "Login" }
               });
            }
            else
            {
                AplicationUser.NombreCompleto = db.tb_Orga_Usuarios.Where(T => T.Usuario == AplicationUser.Usuario).Select(T => T.Nombres + " " + T.Apellidos).SingleOrDefault();
            }

        }
    }
}