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
        public IActionResult Login([FromForm] UserLoginDto dto, [FromServices] JWTManager manager, [FromServices] IHttpContextAccessor ctx)
        {
            //return RedirectToAction("Index", dto);
            var tokens = manager.MakeTokens(dto.Username, dto.Password);
            ctx.HttpContext.Response.Headers["set-cookie"]  = "access=" + tokens[0];
            return Ok(new { access = tokens[0], refresh = tokens[1] });
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateUserDto dto, [FromServices] ICreateUserCommand command)
        {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }
    }
}
