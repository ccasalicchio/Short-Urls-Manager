using Shorten_Urls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shorten_Urls.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Why Bother?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you really have to";

            return View();
        }
    }
}