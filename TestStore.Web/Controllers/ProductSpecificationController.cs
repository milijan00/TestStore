using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;

namespace TestStore.Web.Controllers
{
    public class ProductSpecificationController : Controller
    {
        private UsecaseHandler _handler;

        public ProductSpecificationController(UsecaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public IActionResult Store([FromForm] ProductSpecificationsDto dto, [FromServices] ICreateProductSpecificationCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        [HttpDelete]
        public IActionResult Delete([FromForm] DeleteProductSpecificationDto dto, [FromServices] IDeleteProductSpecificationCommand command)
        {
            this._handler.HandleCommand(command, dto);
            return NoContent(); 
        }
    }
}
