using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
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
       public IActionResult Edit(int id, [FromServices] IGetUserQuery query)
        {
            var user = this._handler.HandleQuery(query, id);
                
            return View(user);
        } 
        [HttpPatch]
        public IActionResult Update([FromForm] UserDto dto, [FromServices] IUpdateUserCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
