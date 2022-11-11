using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class UsecasesController : Controller
    {
        private UsecaseHandler _handler;
        public UsecasesController(UsecaseHandler handler)
        {
            this._handler = handler;
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetUsecasesQuery query)
        {
                return Ok(this._handler.HandleQuery(query));
        }
        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetUsecaseQuery query)
        {
                return Ok(this._handler.HandleQuery(query, id));
        }
        [HttpPost]
        public IActionResult Create([FromForm] UsecaseDto dto, [FromServices] ICreateUsecaseCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromForm] UsecaseDto dto, [FromServices] IUpdateUsecaseCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteUsecaseCommand command)
        {
                this._handler.HandleCommand(command, id);
                return NoContent();
        }
    }
}
