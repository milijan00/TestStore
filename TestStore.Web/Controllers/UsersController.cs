using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class UsersController : Controller
    {
        private UsecaseHandler _handler;

        public UsersController(UsecaseHandler handler)
        {
            _handler = handler;
        }
       
        [HttpGet]
       public IActionResult Edit(int id)
        {
            return View();
        } 
        [HttpPatch]
        public IActionResult Update([FromForm] UserDto dto, [FromServices] IUpdateUserCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
