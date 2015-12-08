using System;
using System.Web.Mvc;
using WebReader.Utilities;

namespace WebReader.Controllers
{
    public class ReadController : Controller
    {
        public ActionResult Index(string uri, string accept)
        {
            Uri baseUri = Request.Url;
            return ClientUtilities.GetWebResponse(uri, accept, baseUri);
        }        
    }
}