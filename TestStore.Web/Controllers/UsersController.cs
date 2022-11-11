using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class UsersController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;

        public UsersController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }

        [HttpGet]
       public IActionResult Edit(int id, [FromServices] IGetUserQuery query)
        {
            if (this._service.Authenticated)
            {
                var user = this._handler.HandleQuery(query, id);
                    
                return View(user);
            }
            return RedirectToAction("Index", "Auth");
        } 
        [HttpPatch]
        public IActionResult Update([FromForm] UserDto dto, [FromServices] IUpdateUserCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
