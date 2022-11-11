using Microsoft.AspNetCore.Mvc;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class AdminpanelController : Controller
    {
        private AuthService _service;
        public AdminpanelController(AuthService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            if (this._service.Authenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
