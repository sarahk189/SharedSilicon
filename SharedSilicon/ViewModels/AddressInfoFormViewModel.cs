using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.ViewModels
{
    public class AddressInfoFormViewModel
    {
        
        [DataType(DataType.Text)]
        [Display(Name = "Address Line 1", Prompt = "Enter your address", Order = 0)]
        [Required(ErrorMessage = "Address is required")]
        public string Addressline_1 { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Address Line 2", Prompt = "Enter your address", Order = 1)]
        public string? Addressline_2 { get; set; }

        [Display(Name = "Postal Code", Prompt = "Enter your postal code", Order = 2)]
        [Required(ErrorMessage = "Postal code is required")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "City", Prompt = "Enter your city", Order = 4)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;
    }

}
