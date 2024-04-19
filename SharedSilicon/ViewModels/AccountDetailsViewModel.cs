using Infrastructure.Entities;
using SharedSilicon.Models;

namespace SharedSilicon.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";
        public BasicInfoFormViewModel BasicInfo { get; set; } = null!;
         
        public AddressInfoFormViewModel AddressInfo { get; set; } = null!;
        public bool IsExternalAccount { get; set; }
    }
}
