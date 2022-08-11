namespace Contacts.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<ContactDTO> Contacts { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Contacts = new List<ContactDTO>();
            foreach (var item in user.Contacts)
            {
                Contacts.Add(new ContactDTO(item.Name, item.PhoneNumber, item.Email,item.Id));
            }
        }
    }
}
