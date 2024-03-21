using ASPNETAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace ASPNETAssignment.ViewModels;

public class SignUpViewModel
{
    public string PageTitle { get; set; } = "Sign up";

    //public SignUpViewModel Form { get; set; } = new SignUpViewModel();
    public SignUpModel Form { get; set; } = new SignUpModel();


    [Display(Name = "Terms and Conditions")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Please agree to the terms and conditions.")]
    public bool TermsAndConditions { get; set; } = false; 

    public SignUpViewModel()
    {

    }
}
