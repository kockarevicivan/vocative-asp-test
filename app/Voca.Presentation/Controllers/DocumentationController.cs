using System.Web.Mvc;

namespace Voca.Presentation.Controllers
{
    public class DocumentationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}