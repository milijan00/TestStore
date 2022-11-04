using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class SpecificationValueController : Controller
    {
        private UsecaseHandler _handler;

        public SpecificationValueController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetAllSpecificationsValuesQuery query)
        {
            return Ok(this._handler.HandleQuery(query));
        }
        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetSpecificationValuesQuery query)
        {
            return Ok(this._handler.HandleQuery(query, id));
        }

        [HttpGet]
         public IActionResult Edit([FromQuery] SpecificationValueDto dto, [FromServices] IGetSpecificationsQuery query)
        {
            var specifications = this._handler.HandleQuery(query);
            var data = new EditSpecficationValueDto
            {
                Dto = dto,
                Specifications = specifications
            };
            return View(data);
        }
        [HttpPost]
        public IActionResult Store([FromForm] SpecificationValueDto dto, [FromServices] ICreateSpecificationValueCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        [HttpGet]
        public IActionResult Create([FromServices] IGetSpecificationsQuery query)
        {
            var specifications = this._handler.HandleQuery(query);
            return View(specifications);
        }

        [HttpPut]
        public IActionResult Update([FromForm] SpecificationValueDto dto, [FromServices] IUpdateSpecificationValueCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete ([FromQuery] SpecificationValueDto dto, [FromServices] IDeleteSpecificationValueCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
