using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.EntityFrameworkCore;
using LapShop.Bl;


namespace LapShop.Controllers
{

    public class HomeController : Controller
    {
        IItem oClsItems;
        ISliders oClsSlider;
        ICategories oClsCateories;
        public HomeController(IItem item , ISliders slider , ICategories categories)
        {
            oClsItems = item;
            oClsSlider = slider;
            oClsCateories = categories;
        }
        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = oClsItems.GetAllItemsData(null).Skip(20).Take(20).ToList();
            vm.lstRecommendedItem = oClsItems.GetAllItemsData(null).Skip(60).Take(10).ToList();
            vm.lstNewItems = oClsItems.GetAllItemsData(null).Skip(90).Take(10).ToList();
            vm.lstFreeDelivryItmes = oClsItems.GetAllItemsData(null).Skip(200).Take(10).ToList();
            vm.lstSliders = oClsSlider.GetAll();
            vm.lstCategories = oClsCateories.GetAll().Take(4).ToList();
            

            return View(vm);
        }
    }
}
