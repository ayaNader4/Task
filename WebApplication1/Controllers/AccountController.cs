using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn)
        {
            this.userManager = userManager;
            this.signInManager = signIn;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult AddUser()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(Register user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser Appuser = new IdentityUser
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    PasswordHash = user.Password
                };
                IdentityResult result = await userManager.CreateAsync(Appuser, user.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Appuser, user.Role);

                    await signInManager.SignInAsync(Appuser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {

            if (ModelState.IsValid)
            {
                IdentityUser result = await userManager.FindByEmailAsync(user.Email);
                if (result != null)
                {
                    bool found = await userManager.CheckPasswordAsync(result, user.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(result, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email or Password is wrong");
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
