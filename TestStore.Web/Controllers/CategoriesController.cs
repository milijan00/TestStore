using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private UsecaseHandler _handler;
        private Core.AuthService _service;
        public CategoriesController(UsecaseHandler handler, Core.AuthService service)
        {
            _handler = handler;
            _service = service;
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
        public IActionResult Edit(int id, [FromServices] IGetCategoryQuery query)
        {
            if (this._service.Authenticated)
            {
                var category = this._handler.HandleQuery(query, id);
                return View(category);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetCategoriesQuery query)
        {
                var result = this._handler.HandleQuery(query);
                return Ok(result);
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetCategoryQuery query)
        {
            if (this._service.Authenticated)
            {
                var result = this._handler.HandleQuery(query, id);
                return Ok(result);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult Store([FromForm] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPut]
        public IActionResult Update([FromForm] CategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
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
