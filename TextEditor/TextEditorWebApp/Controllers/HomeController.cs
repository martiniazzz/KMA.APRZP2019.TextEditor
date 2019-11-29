using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TextEditorWebApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets home page of the web application
        /// </summary>
        /// <returns>View that is rendered to response</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
