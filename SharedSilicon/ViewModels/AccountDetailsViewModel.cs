using Infrastructure.Entities;
using SharedSilicon.Models;

namespace SharedSilicon.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel();
         
        public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
    }
}
