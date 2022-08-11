using Contacts.Models.DTOs;

namespace Contacts.Models.ViewModels
{
    public class ContactsViewModel
    {
        public UserDTO User { get; set; }
        public ResponseMessage ResponseMessage { get; set; }

        public ContactsViewModel(User user)
        {
            User = new UserDTO(user);
            ResponseMessage = new ResponseMessage();
        }
    }
}
