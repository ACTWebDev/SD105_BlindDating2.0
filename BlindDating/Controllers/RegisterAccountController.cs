using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BlindDating.Controllers
{
    public class RegisterAccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public RegisterAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateUser(string username, string password)
        {
            IdentityUser newUser = new IdentityUser();
            newUser.UserName = username;
            newUser.Email = username;

            IdentityResult result = _userManager.CreateAsync(newUser, password).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(newUser,
                                   "NormalUser").Wait();

                _signInManager.SignInAsync(newUser, false).Wait();
            }

            return RedirectToAction("Create", "DatingProfiles");
        }
    }
}