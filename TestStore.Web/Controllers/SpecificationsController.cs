using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class SpecificationsController : Controller
    {
        private UsecaseHandler _handler;

        public SpecificationsController(UsecaseHandler handler)
        {
            _handler = handler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetSpecificationsQuery query)
        {
            var result = this._handler.HandleQuery(query);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetSpecificationQuery query)
        {
            var specification = this._handler.HandleQuery(query, id);
            return Ok(specification);
        }

        [HttpPost]
        public IActionResult Store([FromForm] SpecificationDto dto, [FromServices] ICreateSpecificationCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromForm] SpecificationDto dto, [FromServices] IUpdateSpecificationCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecificationCommand command)
        {
            this._handler.HandleCommand(command, id);
            return NoContent();
        }

    }
}
