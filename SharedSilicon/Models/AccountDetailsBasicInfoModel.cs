using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.Models;

public class AccountDetailsBasicInfoModel
{
     
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone number", Prompt = "Enter your phone number", Order = 3)]
    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "Phone number is required")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Biography", Prompt = "Add a short bio...", Order = 4)]
    [DataType(DataType.MultilineText)]
    public string? Biography { get; set; }
}
