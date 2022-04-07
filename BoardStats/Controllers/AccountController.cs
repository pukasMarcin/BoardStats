using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BoardStats.Data.ViewModels;
using BoardStats.Utility;
using BoardStats.Data.Services;

namespace BoardStats.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        private readonly ApplicationDbContext _db;
        private readonly IPlayersService _service;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>
              signInManager, RoleManager<IdentityRole> roleManager, IPlayersService service)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _service = service;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(RoleHelper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleHelper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(RoleHelper.User));
  

            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {

                    Email = model.Email,
                    UserName = model.Name,
                  

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleHelper.User);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    await _service.AddNewPlayerAsync(user.UserName);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);

        }


[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {

                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.Rem, false) ;
                if(result.Succeeded)
                {
                
                    
                    return RedirectToAction("Index", "Games");
                }
                ModelState.AddModelError(string.Empty, "Invalid login");

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }




}
