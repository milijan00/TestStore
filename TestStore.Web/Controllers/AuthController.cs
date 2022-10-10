using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class AuthController : Controller
    {
        private UsecaseHandler _handler;
        public AuthController(UsecaseHandler handler)
        {
            _handler = handler;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] UserLoginDto dto, [FromServices] JWTManager manager)
        {
            //return RedirectToAction("Index", dto);
            try
            {
                var tokens = manager.MakeTokens(dto.Username, dto.Password);
                return Ok(new { access = tokens[0], refresh = tokens[1] });
            }catch(UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateUserDto dto, [FromServices] ICreateUserCommand command)
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
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }
    }
}
