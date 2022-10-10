using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Usecases.Queries;
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
    }
}
