using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class SpecificationsController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;

        public SpecificationsController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }


        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IGetSpecificationQuery query)
        {
            if (this._service.Authenticated)
            {
                var specification = this._handler.HandleQuery(query, id);
                return View(specification);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this._service.Authenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Auth");
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
            if (this._service.Authenticated)
            {
                var specification = this._handler.HandleQuery(query, id);
                return Ok(specification);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult Store([FromForm] SpecificationDto dto, [FromServices] ICreateSpecificationCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPut]
        public IActionResult Update([FromForm] SpecificationDto dto, [FromServices] IUpdateSpecificationCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecificationCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

    }
}
