namespace Contacts.Models.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ContactDTO()
        {
        }

        public ContactDTO(string name, string phoneNumber, string email, int id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Id = id;

        }
    }
}
