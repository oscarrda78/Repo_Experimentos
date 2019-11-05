using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroWeb.AuthFolder;
using MetroWeb.Models;
namespace MetroWeb.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try{

                var usuario=Session["Usuario"].ToString();
                
                if(usuario != null)
                {
                    ViewBag.RutaLayout = VariablesGlobales.LoggedInLayoutPath;
                }
                else { ViewBag.RutaLayout = "~/Views/Shared/_Layout.cshtml"; }
                
            }
            catch
            {
                
                ViewBag.RutaLayout = "~/Views/Shared/_Layout.cshtml";
            }
     
            return View();
        }

        [UserAuthentication]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [UserAuthentication]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}