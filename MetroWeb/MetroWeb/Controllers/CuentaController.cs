using MetroWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroWeb.Controllers
{
    public class CuentaController : Controller
    {
        private BDGestionIntegradaEntitiesContext db = new BDGestionIntegradaEntitiesContext();
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (model.CuentaValida(db)) 
            {
                Session["Usuario"] = model.Usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
            
        }
        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Registrar()
        {
            return View();
        }
    }
}