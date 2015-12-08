using System;
using System.Web.Mvc;

namespace WebReader.Samples.Controllers
{
    public class DemoController : Controller
    {
        // GET: Mvc
        public ActionResult Index()
        {
            var date = DateTime.Now;
            ViewBag.Date = string.Format("The current server time is {0}, {1}", date.ToString("dd MMMM yyyy"), date.ToString("HH:mm:ss"));
             
            return View();
        }
    }
}