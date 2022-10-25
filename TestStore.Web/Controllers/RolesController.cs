using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class RolesController : Controller
    {
        private UsecaseHandler _handler;

        public RolesController(UsecaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetRolesQuery query)
        {
            return Ok(this._handler.HandleQuery(query));
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetRoleQuery query)
        {
            return Ok(this._handler.HandleQuery(query, id));
        }
        [HttpPost]
        public IActionResult Store([FromForm] RoleDto dto, [FromServices] ICreateRoleCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        [HttpPut]
        public IActionResult Update([FromForm] RoleDto dto, [FromServices] IUpdateRoleCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(204);
        }
        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleCommand command)
        {
            this._handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
