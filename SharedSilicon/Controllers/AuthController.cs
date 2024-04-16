﻿using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SharedSilicon.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {

        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("/details", "Account");

        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(p => p.ErrorMessage)).ToList();
                // Nu kan du inspektera 'errors'-listan i din debugger för att se vilka felmeddelanden som finns
            }
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

        return RedirectToAction("SignIn", "Auth");
    }


    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {

        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        var viewModel = new SignInViewModel();
        return View(viewModel);
    }


	[Route("/signin")]
	[HttpPost]
	public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
	{
		ModelState.Remove("returnUrl");

		if (string.IsNullOrEmpty(returnUrl))
		{
			returnUrl = "/Home/Index";
		}
		ViewData["ReturnUrl"] = returnUrl;
		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);

			if (result.Succeeded)
			{
				// Removed the code that retrieves the access token

				return RedirectToAction("Details", "Account");
			}
			else
			{
				// Sign-in was not successful, add an error
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(viewModel);
			}
		}

		// If we got this far, something failed, redisplay form
		ViewData["StatusMessage"] = "Incorrect e-mail and password";
		return View(viewModel);
	}


	[Route("/signout")]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {

        await _signInManager.SignOutAsync();
        return RedirectToAction("Signin", "Auth");

    }

    #region External Account | Facebook

    [HttpGet]
    public IActionResult Facebook(string provider)
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                    return RedirectToAction("Details", "Account");
            }
        }
        ModelState.AddModelError("InvalidFacebookAuthentication", "danger|Failed to authenticate with Facebook");
        ViewData["StatusMessage"] = "danger|Failed to authenticate with Facebook";
        return RedirectToAction("SignIn", "Auth");

    }

    #endregion

}


