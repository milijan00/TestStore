using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class CartController : Controller
    {
        private UsecaseHandler _handler;
        public CartController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteCartCommand command)
        {
            this._handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Store([FromForm] CartDto dto, ICreateCartCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
    }
}
