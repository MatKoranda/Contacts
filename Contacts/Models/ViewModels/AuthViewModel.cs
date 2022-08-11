using Contacts.Models;
namespace Contacts.Models.ViewModels
{
    public class AuthViewModel
    {
        public ResponseMessage ResponseMessage { get; set; }

        public AuthViewModel()
        {
            ResponseMessage = new ResponseMessage();
        }
    }
}
