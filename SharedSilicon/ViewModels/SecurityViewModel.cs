using SharedSilicon.Helpers;
using SharedSilicon.Models;

namespace SharedSilicon.ViewModels
{
    public class SecurityViewModel
    {
        public string Title { get; set; } = "Security";

        public ChangePasswordModel Password { get; set; } = new ChangePasswordModel();

        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;

        [CheckboxRequired(ErrorMessage = "Your must agree to delete account to continue.")]
        public bool DeleteAccount { get; set; } = false;
    }

}
