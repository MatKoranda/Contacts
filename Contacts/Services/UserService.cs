using Contacts.Database;
using Contacts.Models;
using Contacts.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public class UserService
    {
        private AppDbContext database { get; set; }
        public UserService(AppDbContext database)
        {
            this.database = database;
        }
        public ResponseMessage RegisterUser(string email, string password, out bool isValid)
        {
            if (GetUserByEmail(email) != null)
            {
                isValid = false;
                return new ResponseMessage("Email is already registered");
            }
            User user = new(email, password);
            database.Users.Add(user);
            database.SaveChanges();
            isValid = true;
            return new ResponseMessage("User was successfuly registered");
        }

        public User GetUserById(int? userId)
        {
            return database.Users.Include(u => u.Contacts).FirstOrDefault(u => u.Id == userId);
        }
        public User GetUserByEmail(string userEmail)
        {
            return database.Users.Include(u => u.Contacts).FirstOrDefault(u => u.Email == userEmail);
        }
    }
}
