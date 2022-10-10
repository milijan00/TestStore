using Microsoft.AspNetCore.Mvc;

namespace TestStore.Web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
