using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestStore.Application.Dto;
using TestStore.Application.Dto.searches;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;
using TestStore.Web.Models;

namespace TestStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UsecaseHandler _handler;
        public HomeController(ILogger<HomeController> logger, UsecaseHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        public IActionResult Index([FromServices] IGetProductsQuery query, [FromServices] IGetCategoriesQuery getCategories, [FromServices] IGetBrandsQuery getBrands)
        {
            var searchProduct = new SearchProductsDto();
            var products = this._handler.HandleQuery(query, searchProduct);

            var categories = this._handler.HandleQuery(getCategories);

            var brands = this._handler.HandleQuery(getBrands);
            var result = new ProductsCategoriesBrandsDto
            {
                Products = products,
                Categories = categories,
                Brands = brands
            };
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}