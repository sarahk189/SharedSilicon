using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.Models;

public class ChangePasswordModel
{
    [DataType(DataType.ImageUrl)]
    public string? ProfileImage { get; set; }

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

    [Display(Name = "Current Password", Prompt = "Enter your current password", Order = 0)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "New Password", Prompt = "Enter your new password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "New password is required")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your new password", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Confirmation is required")]
    public string ConfirmPassword { get; set; } = null!;
}
