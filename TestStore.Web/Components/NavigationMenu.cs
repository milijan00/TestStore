using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;

namespace TestStore.Web.ViewComponents
{
    public class NavigationMenu : ViewComponent
    {
        private  readonly TestStoreDbContext _context;
        public NavigationMenu(TestStoreDbContext context)
        {
            this._context = context;
        }

        public IViewComponentResult Invoke()
        {
            var links = this._context.NavLinks.Select(x => new NavLinkDto
            {
                Name = x.Name,
                Action = x.Action,
                Controller = x.Controller
            }).ToList();
            return View(links);
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
