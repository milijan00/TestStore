using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{

    public class CartController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;
        public CartController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            this._service = service;
        }
        public IActionResult Index()
        {
            if (this._service.Authenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteCartCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult Store([FromForm] CartDto dto, [FromServices] ICreateCartCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
