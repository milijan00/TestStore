using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.DataAccess;
using TestStore.Implementation.Exceptions;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class NavLinksController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;
        public NavLinksController(UsecaseHandler handler, AuthService service)
        {
            _handler = handler;
            _service = service;
        }

        public IActionResult Create()
        {
            if (this._service.Authenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Edit(int id, [FromServices] IGetNavLinkQuery query)
        {
            if (this._service.Authenticated)
            {
                var link = this._handler.HandleQuery(query, id);
                return View(link);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public IActionResult Get([FromServices] IGetNavLinksQuery query)
        {
                return Ok(this._handler.HandleQuery(query));
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetNavLinkQuery query)
        {
            if (this._service.Authenticated)
            {
                return Ok(this._handler.HandleQuery(query, id));
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public IActionResult Store([FromForm] NavLinkDto dto, [FromServices] ICreateNavLinkCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPatch]
        public IActionResult Update([FromForm] NavLinkDto dto, [FromServices] IUpdateNavLinkCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteNavLinkCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
