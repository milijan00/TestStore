using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;
using TestStore.Web.Core;

namespace TestStore.Web.ViewComponents
{
    public class NavigationMenu : ViewComponent
    {
        private  readonly TestStoreDbContext _context;
        private readonly AuthService _service;
        public NavigationMenu(TestStoreDbContext context,  AuthService service)
        {
            this._context = context;
            this._service = service;
        }

        public IViewComponentResult Invoke()
        {

            this._service.RetrieveCookieFromRequest();
            var data = new NavLinksUserDataDto();
            if (this._service.Authenticated)
            {
                data.Username = this._service.Token.Claims.First(x => x.Type == "Username").Value;
                data.UserId = Int32.Parse(  this._service.Token.Claims.First(x => x.Type == "UserId").Value);
                data.IsAdmin = this._service.Token.Claims.First(x => x.Type == "Role").Value == "Administrator";
            }
            data.NavLinks = this._context.NavLinks.Select(x => new NavLinkDto
            {
                Name = x.Name,
                Action = x.Action,
                Controller = x.Controller
            }).ToList();
            return View(data);
        }
        //public static   Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult GetNavLinks()
        //{
        //    var navLinks = _context.NavLinks.Select(x => new NavLinkDto
        //    {
        //        Name = x.Name,
        //        Action = x.Action,
        //        Controller = x.Controller
        //    }).ToList();
        //    return View(navLinks);
        //}
    }
}
