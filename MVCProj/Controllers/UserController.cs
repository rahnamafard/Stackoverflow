using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProj.Models;

namespace MVCProj.Controllers
{
    public class UserController : Controller
    {
        StackContext db;

        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;

        public UserController(StackContext db, UserManager<User> userManager, SignInManager<User> signManager)
        {
            this.db = db;
            _userManager = userManager;
            _signManager = signManager;
        }

      

        public IActionResult Index()
        {
            return RedirectToPage("Login");
        }

        public IActionResult Register(string username, string password)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Profile(string id)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                if (User.Identity.IsAuthenticated)
                {
                    User myUser = db.Users.Find(_userManager.GetUserId(User));

                    if (myUser != null)
                    {
                        UserProfileViewModel upvm = new UserProfileViewModel()
                        {
                            Username = myUser.UserName,
                            Score = myUser.Score,
                            Questions = db.Questions.Where(q => q.UserId == myUser.Id).ToList(),
                            Answers = db.Answers.Where(a => a.UserId == myUser.Id).ToList()
                        };
                        return View(upvm);
                    }
                }
                return View("LoginToContinue");
            }

            User user = db.Users.Find(id);

            if(user != null)
            {
                UserProfileViewModel upvm = new UserProfileViewModel()
                {
                    Username = user.UserName,
                    Score = user.Score,
                    Questions = db.Questions.Where(q => q.UserId == user.Id).ToList(),
                    Answers = db.Answers.Where(a => a.UserId == user.Id).ToList()
                };
                return View(upvm);
            }

            return View("LoginToContinue");

        }

        public IActionResult All()
        {
            if(User.Identity.IsAuthenticated)
            {
                var UserList = db.Users.ToList();
                return View(UserList);
            }
            else
            {
                return View("LoginToContinue");
            }
        }
    }
}