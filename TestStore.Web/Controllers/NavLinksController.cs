using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class NavLinksController : Controller
    {
        private UsecaseHandler _handler;

        public NavLinksController(UsecaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetNavLinksQuery query)
        {
                return Ok(this._handler.HandleQuery(query));
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetNavLinkQuery query)
        {
                return Ok(this._handler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Store([FromForm] NavLinkDto dto, [FromServices] ICreateNavLinkCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
        }

        [HttpPatch]
        public IActionResult Update([FromForm] NavLinkDto dto, [FromServices] IUpdateNavLinkCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteNavLinkCommand command)
        {
                this._handler.HandleCommand(command, id);
                return NoContent();
        }
    }
}
