﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiome.Models;
using MyBiome.Models.ViewModels;
using NToastNotify;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyBiome.Infrastructure;

namespace MyBiome.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
		private readonly IToastNotification _toastNotification;
        private readonly DataContext _context;

        public OrderController(UserManager<AppUser> userManager,
		 SignInManager<AppUser> signInManager, IToastNotification toastNotification, DataContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_toastNotification = toastNotification;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var order = await _context.Orders
                .Where(f => f.UserId == id)
                .ToListAsync();

            return View(order);
        }



    }
}
