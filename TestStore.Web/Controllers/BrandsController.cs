using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class BrandsController : Controller
    {
        private UsecaseHandler _handler;
        public BrandsController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetBrandsQuery query)
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
        public IActionResult Find(int id, [FromServices] IGetBrandQuery query)
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
        public IActionResult Store ([FromForm] BrandDto dto, [FromServices] ICreateBrandCommand command)
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
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Update([FromForm] BrandDto dto, [FromServices] IUpdateBrandCommand command)
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
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
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
