using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager , 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        
        
        {
            UserModel model = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View();
        }
        public async Task< IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Register()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {

            if (!ModelState.IsValid)
                return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    var myUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(myUser, "Customer");

                    if (loginResult.Succeeded)
                        return Redirect("/Order/OrderSuccess");

                }

                else
                {

                }
            }
            catch
            {

            }
                return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model , string returnUrl)
        {

            //if (!ModelState.IsValid)
            //    return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {

                var loginResult = 
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (loginResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("~/");
                    else
                        return Redirect(model.ReturnUrl);

                }
                return Redirect("/Order/OrderSuccess");
            }
            catch
            {

            }
            return View(new UserModel());
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
