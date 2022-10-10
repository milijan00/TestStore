using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class NavLinksController : Controller
    {
        private UsecaseHandler _handler;

        public NavLinksController(UsecaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetNavLinksQuery query)
        {
            try
            {
                return Ok(this._handler.HandleQuery(query));
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetNavLinkQuery query)
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
        public IActionResult Post([FromForm] NavLinkDto dto, [FromServices] ICreateNavLinkCommand command)
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

        [HttpPatch]
        public IActionResult Update([FromForm] NavLinkDto dto, [FromServices] IUpdateNavLinkCommand command)
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
        public IActionResult Delete(int id, [FromServices] IDeleteNavLinkCommand command)
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
