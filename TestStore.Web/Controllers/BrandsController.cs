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
                var result = this._handler.HandleQuery(query);
                return Ok(result);
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetBrandQuery query)
        {
                var result = this._handler.HandleQuery(query, id);
                return Ok(result);
        }

        [HttpPost]
        public IActionResult Store ([FromForm] BrandDto dto, [FromServices] ICreateBrandCommand command)
        {
              this._handler.HandleCommand(command, dto);
              return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Update([FromForm] BrandDto dto, [FromServices] IUpdateBrandCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
        {
                this._handler.HandleCommand(command, id);
                return NoContent();
        }
    }
}
