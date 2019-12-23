using System.Web.Mvc;

namespace Voca.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.HeaderClass = "transparent";

            return View();
        }
    }
}