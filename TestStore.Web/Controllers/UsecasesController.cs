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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetUsecasesQuery query)
        {
            try
            {
                return Ok(this._handler.HandleQuery(query));
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetUsecaseQuery query)
        {
            try
            {
                return Ok(this._handler.HandleQuery(query, id));
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
        public IActionResult Create([FromForm] CreateUsecaseDto dto, [FromServices] ICreateUsecaseCommand command)
        {
            try
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }catch(UnprocessableEntityException ex)
            {
                return UnprocessableEntity(ex.Data);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Update([FromForm] UpdateUsecaseDto dto, [FromServices] IUpdateUsecaseCommand command)
        {
            try
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }catch(UnprocessableEntityException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteUsecaseCommand command)
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
