using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Usecases;
using TestStore.Implementation.Exceptions;
using TestStore.Application.Usecases.Queries;
using TestStore.Application.Dto.searches;

namespace TestStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private UsecaseHandler _handler;
        private List<string> extensions = new List<string>() { ".jpeg", ".jpg", ".png" };
        public ProductsController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetProductsQuery query, [FromQuery] SearchProductsDto dto)
        {
            try
            {
                var result = this._handler.HandleQuery(query, dto);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetProductQuery query)
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
        public IActionResult Store([FromForm] ProductWithImageDto dto, [FromServices] ICreateProductCommand command)
        {
            try
            {
                // upload image to the server

                string fileName = this.UploadImageToServer(dto.Image);
                dto.ImageName = fileName;
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

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
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

        [HttpPatch]
        public IActionResult Update([FromServices] IUpdateProductCommand command, [FromForm] ProductWithImageDto dto)
        {
            try
            {
                if(dto.Image != null)
                {
                    string fileName = this.UploadImageToServer(dto.Image);
                    dto.ImageName = fileName;
                }
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
        [HttpPost]
        public IActionResult ImageUpload([FromForm] ProductImageDto dto, [FromServices] IUpdateProductCommand command)
        {
            try
            {
                string fileName = this.UploadImageToServer(dto.Image);
                var product = new ProductDto { ImageName = fileName, Id = dto.Id };
                this._handler.HandleCommand(command, product);
                return NoContent();
            }catch(UnprocessableEntityException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
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
    }
}
