using Bl;
using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        IItem itemService;
        UserManager<ApplicationUser> _userManager;
        ISalesInvoice _salesInvoiceService;
        public OrderController(IItem itemsService  , UserManager<ApplicationUser> userManager ,
            ISalesInvoice salesInvoiceService)
        {
            itemService = itemsService;
            _userManager = userManager;
            _salesInvoiceService = salesInvoiceService;
        }
        public IActionResult Cart()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            return View(cart);
        }

        public IActionResult MyOrders()
        {
            return View();
        }

        [Authorize]
        public IActionResult OrderSuccess()
        {
            return View();
        }


        public IActionResult AddToCart(int ItemId)
        {
            ShoppingCart cart;
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();
            var item = itemService.GetItemId(ItemId);


            var itemInList = cart.LstItems.Where(a => a.ItemId == ItemId).FirstOrDefault();

            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;

            }
            else
            {
                cart.LstItems.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice

                });
            }

            cart.Total = cart.LstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart)  );
            return RedirectToAction("Cart");
        }

        public async Task SaveOrder(ShoppingCart oShoppingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();

                foreach(var item in oShoppingCart.LstItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);
                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                };

                _salesInvoiceService.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
