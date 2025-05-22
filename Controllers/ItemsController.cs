using Bl;
using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {        
        IItem oItem;
        IItemImages oItemImage;
        public ItemsController(IItem iItem , IItemImages iItemImage)
        {
            oItem = iItem;
            oItemImage = iItemImage;

        }
        public IActionResult ItemDetails(int id)
        {
            var item = oItem.GetItemId(id);
            VmItemDetails vm = new VmItemDetails();

            vm.Item = item;
            vm.lstRecommendedItems = oItem.GetRecommendedItems(id).Take(10).ToList();
            vm.lstItemImages = oItemImage.GetByItemId(id);
            return View(vm);
        }

        public IActionResult ItemList()
        {
         
            return View();
        }
    }
}
