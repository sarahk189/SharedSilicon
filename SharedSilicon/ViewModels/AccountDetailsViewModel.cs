using SharedSilicon.Models;

namespace SharedSilicon.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account Details";

        public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
        {
            ProfileImage = "images/contactDetailsImages/profile_image.svg",
            FirstName = "Micaela",
            LastName = "Nilsson",
            Email = "micaela.nilsson@domain.com"
        };
         
        public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
    }
}
