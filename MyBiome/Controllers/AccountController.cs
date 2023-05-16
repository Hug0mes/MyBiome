using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiome.Models;
using MyBiome.Models.ViewModels;

namespace MyBiome.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IActionResult Index()
        {
            return View();
        }


		public AccountController(UserManager<AppUser> userManager,
		 SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View(model);
			}

			await _signInManager.RefreshSignInAsync(user);
			return RedirectToAction(nameof(DashboardController.Index), "Home");
		}

	}
}
