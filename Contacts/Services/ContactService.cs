using Contacts.Database;
using Contacts.Models;
using Contacts.Models.DTOs;

namespace Contacts.Services
{
    public class ContactService
    {
        private AppDbContext database { get; set; }
        public ContactService(AppDbContext database)
        {
            this.database = database;
        }
        public ResponseMessage AddContact(string name, string phoneNumber, string email, User user)
        {
            Contact Contact = new Contact(name, phoneNumber, email, user);
            database.Contacts.Add(Contact);
            database.SaveChanges();
            return new ResponseMessage("contact added");
        }

        public ResponseMessage DeleteContact(int contactId)
        {
            Contact contact = GetContactById(contactId);
            database.Contacts.Remove(contact);
            database.SaveChanges();
            return new ResponseMessage("contact deleted");
        }

        public ResponseMessage UpdateContact(int contactId, string name, string phoneNumber, string email, User user)
        {
            Contact contact = GetContactById(contactId);
            contact.Name = name;
            contact.PhoneNumber = phoneNumber;
            contact.Email = email;
            database.SaveChanges();
            return new ResponseMessage("contact updated");
        }

        public Contact GetContactById(int contactId)
        {
            return database.Contacts.FirstOrDefault(t => t.Id == contactId);
        }
    }
}
