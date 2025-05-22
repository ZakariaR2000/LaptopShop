using LapShop.Bl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class PagesController : Controller
    {

        IPages oClsPages;
        public PagesController(IPages page)
        {
            oClsPages = page;
        }
        // GET: PagesController
        public ActionResult Index(int pageId)
        {
            var page = oClsPages.GetById(pageId);

            if (page == null)
                return NotFound(); // أو يمكنك إعادة توجيه المستخدم إلى صفحة خطأ

            return View(page);
        }




    }
}
