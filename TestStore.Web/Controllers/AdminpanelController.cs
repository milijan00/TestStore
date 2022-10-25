using Microsoft.AspNetCore.Mvc;

namespace TestStore.Web.Controllers
{
    public class AdminpanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
