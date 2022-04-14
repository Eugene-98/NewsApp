﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels;

namespace NewsApp.Controllers;

public class AccountController : Controller
{
	private readonly Context _context;

	public AccountController(Context context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
			if (user == null)
			{
				user = new User { Username = model.Username, Password = model.Password};
				Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
				if (userRole != null)
				{
					user.Role = userRole;
				}

				_context.Users.Add(user);
				await _context.SaveChangesAsync();
				

				await Authenticate(user);

				return Redirect("~/Admin/Index");
			}
			else
			{
				ModelState.AddModelError("", "Invalid login or password");
			}
		}

		return View(model);
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			User user =
				await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
			if (user != null)
			{
				await Authenticate(user);

				return Redirect("~/Admin/Index");
			}
			ModelState.AddModelError("", "Invalid username or password");
		}

		return View(model);
	}
	private async Task Authenticate(User user)
	{
		var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
			};

		ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
	}

	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Login", "Account");
	}
}