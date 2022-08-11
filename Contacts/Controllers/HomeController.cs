using Contacts.Database;
using Contacts.Models;
using Contacts.Models.ViewModels;
using Contacts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        private readonly TokenService tokenService;
        private readonly AppDbContext database;
        private readonly ContactService contactService;
        public HomeController(UserService userService, TokenService tokenService, AppDbContext database, ContactService contactService)
        {
            this.userService = userService;
            this.contactService = contactService;
            this.database = database;
            this.tokenService = tokenService;
        }

        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("JWTToken"))
            {
                return RedirectToAction("Contacts", "Contacts");
            }
            return RedirectToAction("LoginRedirect");
        }

        [HttpGet("Login")]
        public IActionResult LoginRedirect()
        {
            AuthViewModel loginViewModel = new AuthViewModel();
            if (Request.Cookies.ContainsKey("JWTToken"))
            {
                loginViewModel.ResponseMessage.Message = "You are already logged in. Logging under another account will log out the first account.";
            }
            return View("Login", loginViewModel);
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            ResponseMessage responseMessage = tokenService.LoginUser(email, password, out bool isValid);
            if (!isValid)
            {
                AuthViewModel authViewModel = new AuthViewModel();
                authViewModel.ResponseMessage = responseMessage;
                return View("Login", authViewModel);
            }
            if (Request.Cookies.ContainsKey("JWTToken"))
            {
                Response.Cookies.Delete("JWTToken");
            }
            Response.Cookies.Append("JWTToken", responseMessage.Message, new CookieOptions());
            return RedirectToAction("Contacts", "Contacts");
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWTToken");
            return RedirectToAction("LoginRedirect");
        }

        [HttpPost("Register")]
        public IActionResult Register(string email, string password)
        {
            AuthViewModel authViewModel = new AuthViewModel();

            authViewModel.ResponseMessage = userService.RegisterUser(email, password, out bool isValid); ;
            if (isValid)
            {
                return View("Login", authViewModel);
            }
            return View("Register", authViewModel);
        }

        [HttpGet("Register")]
        public IActionResult RegisterRedirect()
        {
            AuthViewModel authViewModel = new AuthViewModel();
            return View("register", authViewModel);
        }

    }
}
