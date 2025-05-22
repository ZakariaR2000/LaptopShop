using LapShop.Bl;
using LapShop.Models;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin , data entry")]
    [Area("admin")]
    public class ItemsController : Controller
    {

        public ItemsController(IItem item , ICategories categories
            , IItemType itemType , IOs os)
        {
            oClsItmes = item;
            oClsCategories = categories;
            oClsItmeTypss = itemType;
            oClsOs = os;
        }
        IItem oClsItmes;
        IItemType oClsItmeTypss;

        ICategories oClsCategories;
        IOs oClsOs;

        [AllowAnonymous]
        public IActionResult List()
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
            var itmes = oClsItmes.GetAllItemsData(null);

            return View(itmes);
        }

        public IActionResult Search(int id)
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
            var itmes = oClsItmes.GetAllItemsData(id);

            return View("List" , itmes);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int? ItemId)
        {
            var Item = new TbItem();
            ViewBag.lstCategories = oClsCategories.GetAll();
            ViewBag.lstItemTypes = oClsItmeTypss.GetAll();
            ViewBag.lstOs = oClsOs.GetAll();


            if (ItemId != null)
            {
                Item = oClsItmes.GetById(Convert.ToInt32(ItemId));
            }

            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);

            item.ImageName = await Helper.UploadImage(Files, "Items");


            oClsItmes.Save(item);

            return RedirectToAction("List");

        }

        public IActionResult Delete(int itemId)
        {

            oClsItmes.Delete(itemId);
            return RedirectToAction("List");

        }

    }
}
