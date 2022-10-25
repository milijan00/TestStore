using Microsoft.AspNetCore.Mvc;

namespace TestStore.Web.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
