using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Application.Usecases.Commands;
using TestStore.Application.Usecases.Queries;
using TestStore.Implementation.Usecases;
using TestStore.Web.Core;

namespace TestStore.Web.Controllers
{
    public class RolesController : Controller
    {
        private UsecaseHandler _handler;
        private AuthService _service;
        public RolesController(UsecaseHandler handler,AuthService service )
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
        public IActionResult Edit(int id, [FromServices] IGetRoleQuery query)
        {
            if (this._service.Authenticated)
            {
                var role = this._handler.HandleQuery(query, id);
                return View(role);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetRolesQuery query)
        {
            if (this._service.Authenticated)
            {
                return Ok(this._handler.HandleQuery(query));
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public IActionResult Find(int id, [FromServices] IGetRoleQuery query)
        {
            if (this._service.Authenticated)
            {
                return Ok(this._handler.HandleQuery(query, id));
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpPost]
        public IActionResult Store([FromForm] RoleDto dto, [FromServices] ICreateRoleCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpPut]
        public IActionResult Update([FromForm] RoleDto dto, [FromServices] IUpdateRoleCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(204);
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpDelete]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleCommand command)
        {
            if (this._service.Authenticated)
            {
                this._handler.HandleCommand(command, id);
                return StatusCode(204);
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
