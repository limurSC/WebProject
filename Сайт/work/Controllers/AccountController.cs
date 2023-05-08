﻿using DigitalPortfolio.Domain.ViewModel.User;
using DigitalPortfolio.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace DigitalPortfolio.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountServise;

		public AccountController(IAccountService accountServise)
		{
			_accountServise = accountServise;
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
				if(response.StatusCode == Domain.Enum.StatusCode.Ok) 
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
				if(response.StatusCode == Domain.Enum.StatusCode.Ok)
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
	}
}
