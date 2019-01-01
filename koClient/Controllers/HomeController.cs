using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace koClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Carrelli()
        {

            return View("../Carrelli/Index");
        }

        public ActionResult Componenti()
        {
            

            return View("../Componenti/Index");
        }
    }
}