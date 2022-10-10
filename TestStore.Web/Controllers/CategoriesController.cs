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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetCategoriesQuery query)
        {
            try
            {
                var result = this._handler.HandleQuery(query);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetCategoryQuery query)
        {
            try
            {
                var result = this._handler.HandleQuery(query, id);
                return Ok(result);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Store([FromForm] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
            try
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch(UnprocessableEntityException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
        }

        [HttpPut]
        public IActionResult Update([FromForm] CategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
            try
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            catch(UnprocessableEntityException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            try
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
