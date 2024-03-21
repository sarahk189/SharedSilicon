using ASPNETAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETAssignment.Controllers;

public class AuthController : Controller
{
    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public IActionResult SignUp(SignUpViewModel viewModel)
    {     
        if (!ModelState.IsValid) 
            return View(viewModel);

        return RedirectToAction("SignIn", "Auth");
    }




    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }
}
