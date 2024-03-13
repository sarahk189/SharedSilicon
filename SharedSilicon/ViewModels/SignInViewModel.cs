using SharedSilicon.Models;

namespace SharedSilicon.ViewModels;

public class SignInViewModel
{

    public string PageTitle { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }
}
