using SharedSilicon.Models;

namespace SharedSilicon.ViewModels;

public class SignUpViewModel
{
    public string PageTitle { get; set; } = "Sign up";

    public SignUpModel Form { get; set; } = new SignUpModel();


   
    public bool TermsAndConditions { get; set; } = false; 

    public SignUpViewModel()
    {

    }
}
