using ASPNETAssignment.Models;

namespace ASPNETAssignment.ViewModels;

public class SignInViewModel
{

    public string PageTitle { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }
}
