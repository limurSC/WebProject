using DigitalPortfolio.Domain.Entity;
using DigitalPortfolio.Domain.ViewModel.User;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace DigitalPortfolio.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountServise;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AccountController(IAccountService accountServise, IWebHostEnvironment webHostEnvironment)
		{
			_accountServise = accountServise;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _accountServise.Register(model);
				if(response.StatusCode == Domain.Enum.StatusCode.OK) 
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(response.Data));

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Login() => View();

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if(ModelState.IsValid) 
			{
				var response = await _accountServise.Login(model);
				if(response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(response.Data));

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(model);
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
            var email = User.Identity.Name;
            var response = await _accountServise.GetByEmail(email);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }


		[HttpPost]
        public IActionResult Index(User user)
		{
			if(user.Secret!=null)
				user.Image = SaveImage(user.Secret);
            user = _accountServise.Save(user).Data;
			return View(user);
        }
        private string SaveImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
				var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
				fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
				var filePath = Path.Combine(uploadDir, fileName);
				using (var fileStram = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStram);
				}
            }
			return fileName;
        }
    }
}
