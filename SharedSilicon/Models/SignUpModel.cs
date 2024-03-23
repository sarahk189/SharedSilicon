using SharedSilicon.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.Models;

public class SignUpModel
{
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Invalid password, must be strong")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the terms and conditions", Order = 5)]
    [Required(ErrorMessage = "Agree god dammit")]
    [CheckboxRequired(ErrorMessage = "You must accept to proceed")]
    public bool TermsAndConditions { get; set; } = false;

}

