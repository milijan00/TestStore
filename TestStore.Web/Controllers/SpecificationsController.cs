using Microsoft.AspNetCore.Mvc;

namespace TestStore.Web.Controllers
{
    public class SpecificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
