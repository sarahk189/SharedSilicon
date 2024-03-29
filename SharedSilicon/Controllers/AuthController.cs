using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authentication;

namespace SharedSilicon.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

	[Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {

		if (_signInManager.IsSignedIn(User))
			return RedirectToAction("/details", "Account");

		//return View();
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

         
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if (exists)
            {
				
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

        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        //return View();
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }


    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
       
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }
                
        }


        ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(viewModel);
        

        //var result = _authService.SignIn(viewModel.Form);
        //if (result)
        //    return RedirectToAction("Account", "Index");


       

       
    }


	[Route("/signout")]
	[HttpGet]
	public new async Task <IActionResult> SignOut()
	{

		await _signInManager.SignOutAsync();
		return RedirectToAction("SignIn", "Auth");
		//var viewModel = new SignInViewModel();
		//return View(viewModel);
	}



    //[Route("/account/details")]
    //[HttpGet]
    //public async Task<IActionResult> Details()
    //{
    //    if (!_signInManager.IsSignedIn(User))
    //        return RedirectToAction("Details", "Account");

    //    var userEntity = await _userManager.GetUserAsync(User);

    //    var viewModel = new AccountDetailsViewModel();
    //    return View("~/Views/Account/Details.cshtml", viewModel);
    //}

}
