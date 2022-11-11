using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class BrandsController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;
        public BrandsController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }
        public IActionResult Create()
        {
            if (this._service.Authenticated)
            {
                return View();
            }
           return  RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IGetBrandQuery query)
        {
            if (this._service.Authenticated)
            {
                var brand = this._handler.HandleQuery(query, id);
                return View(brand);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetBrandsQuery query)
        {
                var result = this._handler.HandleQuery(query);
                return Ok(result);
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetBrandQuery query)
        {
            if (this._service.Authenticated)
            {
                var result = this._handler.HandleQuery(query, id);
                return Ok(result);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult Store ([FromForm] BrandDto dto, [FromServices] ICreateBrandCommand command)
        {
            if (this._service.Authenticated)
            {
              this._handler.HandleCommand(command, dto);
              return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPut]
        public IActionResult Update([FromForm] BrandDto dto, [FromServices] IUpdateBrandCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
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
