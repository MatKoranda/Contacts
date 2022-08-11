using Contacts.Database;
using Contacts.Models;
using Contacts.Models.DTOs;
using Contacts.Models.ViewModels;
using Contacts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly UserService userService;
        private readonly TokenService tokenService;
        private readonly AppDbContext database;
        private readonly ContactService contactService;
        public ContactsController(UserService userService, TokenService tokenService, AppDbContext database, ContactService contactService)
        {
            this.userService = userService;
            this.contactService = contactService;
            this.database = database;
            this.tokenService = tokenService;
        }
        [HttpGet]
        public IActionResult Contacts()
        {
            User user = userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"]));
            ContactsViewModel contactsViewModel = new ContactsViewModel(user);
            return View("Contacts", contactsViewModel);
        }


        [HttpPost]
        public ActionResult AddContact(string name, string phoneNumber, string email)
        {
            User user = userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"]));
            ContactsViewModel contactsViewModel = new ContactsViewModel(user);
            contactsViewModel.ResponseMessage = contactService.AddContact(name, phoneNumber, email, user);
            contactsViewModel.User = new UserDTO(userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"])));
            return View("Contacts", contactsViewModel);
        }

        [HttpPut]
        public ActionResult UpdateContact(int contactId, string name, string phoneNumber, string email)
        {
            User user = userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"]));
            ContactsViewModel contactsViewModel = new ContactsViewModel(user);
            contactsViewModel.ResponseMessage = contactService.UpdateContact(contactId, name, phoneNumber, email, user);
            contactsViewModel.User = new UserDTO(userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"])));
            return View("Contacts", contactsViewModel);
        }
        [HttpPost]
        public ActionResult DeleteContact(int contactId)
        {
            User user = userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"]));
            ContactsViewModel contactsViewModel = new ContactsViewModel(user);
            contactsViewModel.ResponseMessage = contactService.DeleteContact(contactId);
            contactsViewModel.User = new UserDTO(userService.GetUserById(tokenService.ValidateToken(Request.Cookies["JWTToken"])));
            return View("Contacts", contactsViewModel);
        }


    }
}
