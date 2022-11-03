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
        public CategoriesController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IGetCategoryQuery query)
        {
            var category = this._handler.HandleQuery(query, id);
            return View(category);
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
                var result = this._handler.HandleQuery(query, id);
                return Ok(result);
        }

        [HttpPost]
        public IActionResult Store([FromForm] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromForm] CategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
                this._handler.HandleCommand(command, id);
                return NoContent();
        }
    }
}
