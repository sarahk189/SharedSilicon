using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories;

namespace SharedSilicon.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<UserEntity> _userManager;

    public AuthController(UserManager<UserEntity> userManager)
    {
		_userManager = userManager;
	}



    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid )
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if (exists)
            {
				ModelState.AddModelError("AlreadyExists", "Email already exists");
                ViewData["ErrorMessage"] = "User with the same email address already exists";
				return View(viewModel);
			}

            var userEntity = UserFactory.CreateUser(viewModel.Form.FirstName, viewModel.Form.LastName, viewModel.Form.Email);


            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
            if (result.Succeeded)
            { 
                return RedirectToAction("SignIn", "Auth"); 
            }
            
        }
        //if (!ModelState.IsValid) 
        //    return View(viewModel);

        return RedirectToAction("SignIn", "Auth");
    }




    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpPost]
    public IActionResult SignIn(SignInViewModel viewModel)
    {
       
        if (!ModelState.IsValid)              
            return View(viewModel);
        

        //var result = _authService.SignIn(viewModel.Form);
        //if (result)
        //    return RedirectToAction("Account", "Index");


        viewModel.ErrorMessage = "Incorrect email or password";  
        return View(viewModel);

       
    }
}
