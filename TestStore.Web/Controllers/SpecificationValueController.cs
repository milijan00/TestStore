using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class SpecificationValueController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;

        public SpecificationValueController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetAllSpecificationsValuesQuery query)
        {
            return Ok(this._handler.HandleQuery(query));
        }
        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetSpecificationValuesQuery query)
        {
            if (this._service.Authenticated)
            {
                return Ok(this._handler.HandleQuery(query, id));
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
         public IActionResult Edit([FromQuery] SpecificationValueDto dto, [FromServices] IGetSpecificationsQuery query)
        {
            if (this._service.Authenticated)
            {
                var specifications = this._handler.HandleQuery(query);
                var data = new EditSpecficationValueDto
                {
                    Dto = dto,
                    Specifications = specifications
                };
                return View(data);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpPost]
        public IActionResult Store([FromForm] SpecificationValueDto dto, [FromServices] ICreateSpecificationValueCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Create([FromServices] IGetSpecificationsQuery query)
        {
            if (this._service.Authenticated)
            {
                var specifications = this._handler.HandleQuery(query);
                return View(specifications);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPut]
        public IActionResult Update([FromForm] SpecificationValueDto dto, [FromServices] IUpdateSpecificationValueCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete ([FromQuery] SpecificationValueDto dto, [FromServices] IDeleteSpecificationValueCommand command)
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
