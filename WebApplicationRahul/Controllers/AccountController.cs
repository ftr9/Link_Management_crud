using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationRahul.Data;
using WebApplicationRahul.Models;

namespace WebApplicationRahul.Controllers
{
    public class AccountController : Controller
    {

        public WebApplicationRahulContext dbContext;
        public AccountController(WebApplicationRahulContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userData)
        {

            var registeredUser = this.dbContext.User.FirstOrDefault((u)=>u.Email == userData.Email);
            
            //check if user exists
            if(registeredUser == null)
            {
                ViewData["EmailError"] = "Email does not exists";
                return View();
            }
            //check if password is correct
            if(registeredUser.Password != userData.Password)
            {

                ViewData["PasswordError"] = "Incorrect Password !!";
                return View(userData);
            }

            //Login functionality
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userData.Email)
                };
            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("/Link");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {

                var alreadyRegisteredUser = await this.dbContext.User.FirstOrDefaultAsync((u)=>u.Email
                 == newUser.Email);
                if(alreadyRegisteredUser != null)
                {
                    ViewData["EmailAlreadyPresent"] = "The Email You Provided already exists";
                    return View(newUser);
                }

                await this.dbContext.User.AddAsync(newUser);
                await this.dbContext.SaveChangesAsync();
                return RedirectToAction("Login");
         

            
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
