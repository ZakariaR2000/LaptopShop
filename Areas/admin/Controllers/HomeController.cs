using LapShop.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        [Authorize(Roles ="Admin")]
        [Authorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
