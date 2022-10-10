using Microsoft.AspNetCore.Mvc;
using TestStore.Application.Dto;
using TestStore.Implementation.DataAccess;

namespace TestStore.Web.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private static  readonly TestStoreDbContext _context = new TestStoreDbContext(); 

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
