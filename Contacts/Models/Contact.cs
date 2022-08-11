namespace Contacts.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public User User { get; set; }

        public Contact()
        {
        }

        public Contact(string name, string phoneNumber, string email, User user)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            User = user;
        }
    }
}
