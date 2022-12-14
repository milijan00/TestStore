using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;
using TestStore.Implementation.Exceptions;
using TestStore.Application.Usecases.Queries;
using TestStore.Application.Dto.searches;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private UsecaseHandler _handler;
        private List<string> extensions = new List<string>() { ".jpeg", ".jpg", ".png" };
        private AuthService _service;
        public ProductsController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }
        public IActionResult Create([FromServices] IGetCategoriesQuery getCategories, [FromServices] IGetBrandsQuery getBrands)
        {
            if (this._service.Authenticated)
            {
                var categories = this._handler.HandleQuery(getCategories);
                var brands = this._handler.HandleQuery(getBrands);

                return View(new CreateProductsPageDataDto
                {
                    Categories = categories,
                    Brands = brands
                });
            }
            return RedirectToAction("Index", "Auth");

        }
        [HttpGet]
        public IActionResult Index(int id, [FromServices] IGetProductQuery query )
        {
            var product = this._handler.HandleQuery(query, id);
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IGetProductQuery query, [FromServices] IGetCategoriesQuery getCategories, [FromServices] IGetBrandsQuery getBrands, [FromServices] IGetAllSpecificationsValuesQuery getAllSpecifications)
        {
            if (this._service.Authenticated)
            {
                var categories = this._handler.HandleQuery(getCategories);
                var brands = this._handler.HandleQuery(getBrands);
                var product = this._handler.HandleQuery(query, id);
                var specificationsValues = this._handler.HandleQuery(getAllSpecifications);
                return View(new UpdateProductsPageDataDto
                {
                    Categories = categories,
                    Brands = brands,
                    Product = product ,
                    SpecificationsValues = specificationsValues
                });
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetProductsQuery query, [FromQuery] SearchProductsDto dto)
        {
                var result = this._handler.HandleQuery(query, dto);
                return Ok(result);
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetProductQuery query)
        {
                return Ok(this._handler.HandleQuery(query, id)); 
        }

        [HttpPost]
        public IActionResult Store([FromForm] ProductWithImageDto dto, [FromServices] ICreateProductCommand command)
        {
            if (this._service.Authenticated)
            {
                string fileName = this.UploadImageToServer(dto.Image);
                dto.ImageName = fileName;
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPatch]
        public IActionResult Update([FromServices] IUpdateProductCommand command, [FromForm] ProductWithImageDto dto)
        {
            if (this._service.Authenticated)
            {
                if(dto.Image != null)
                {
                    string fileName = this.UploadImageToServer(dto.Image);
                    dto.ImageName = fileName;
                }
                this._handler.HandleCommand(command, dto);
                return StatusCode(204);
            }
            return RedirectToAction("Index", "Auth");
        }
        private string UploadImageToServer(IFormFile ImageToUlooad)
        {
            if (ImageToUlooad == null)
            {
                throw new  UnprocessableEntityException(new { errorMessage = "You have to send an image." });
            }
            // treba da proverim extenziju

            string extension = Path.GetExtension(ImageToUlooad.FileName);
            if (!this.extensions.Contains(extension))
            {
                throw new UnprocessableEntityException(new { errorMessage = "File extension is unacceptable. Acceptable extensions are .jpeg, .jpg and .png" });
            }
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine("wwwroot", "images", fileName);
            using(var image = Image.Load(ImageToUlooad.OpenReadStream()))
            {
                int[] size = this.GetNewSize(image);
                image.Mutate(h => h.Resize(size[0], size[1]));
                image.Save(filePath);
            }
            return fileName;
        }
        [HttpPatch]
        public IActionResult ImageUpload([FromForm] ProductImageDto dto, [FromServices] IUpdateProductCommand command)
        {
                string fileName = this.UploadImageToServer(dto.Image);
                var product = new ProductDto { ImageName = fileName, Id = dto.Id };
                this._handler.HandleCommand(command, product);
                return NoContent();
        }
        private int[] GetNewSize(Image image, int maxWidth = 250, int maxHeight = 250)
        {
            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                double widthRatio = (double)image.Width / maxWidth;
                double heightRatio = (double)image.Height / maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(image.Width / ratio);
                int newHeight = (int)(image.Height / ratio);
                return new int[] { newWidth, newHeight };
            }else
            {
                return new int[] { image.Width, image.Height};
            }
        }

        [HttpDelete]
        public IActionResult RemoveSpecification([FromQuery] RemoveSpecificationDto dto, [FromServices] IDeleteSpecificationValueFromProductsCollectionCommand command, [FromServices] IGetProductsSpecificationsQuery query)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);

                return Ok(this._handler.HandleQuery(query, dto.ProductId));
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult AddSpecification([FromForm] ProductsSpecificationDto dto, [FromServices] IUpdateProductSpecfiicationCommand command, [FromServices] IGetProductsSpecificationsQuery query)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return Ok(this._handler.HandleQuery(query, dto.ProductId));
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
