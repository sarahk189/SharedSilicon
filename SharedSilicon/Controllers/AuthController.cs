using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Infrastructure.Dtos;

namespace SharedSilicon.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;


    #region Sign Up

    [HttpGet]
    [Route("/signup")]
   
    public IActionResult SignUp()
    {

        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        var viewModel =  new SignUpViewModel();
        return View(viewModel);
    }


    [HttpPost]
    [Route("/signup")]
    
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

        return RedirectToAction("SignIn", "Auth");
    }

    #endregion

    #region Sign in
    [HttpGet]
    [Route("/signin")]
    
    public IActionResult SignIn()
    {

        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        var viewModel = new SignInViewModel();
        return View(viewModel);
    }

	[HttpPost]
	[Route("/signin")]
	public async Task<IActionResult> SignIn(SignInViewModel viewModel)
	{
		if (ModelState.IsValid)
		{
			var user = await _userManager.FindByEmailAsync(viewModel.Form.Email);
			if (user != null)
			{
				var isValidPassword = await _userManager.CheckPasswordAsync(user, viewModel.Form.Password);
				if (isValidPassword)
				{
					var claims = new[]
					{
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
						new Claim(ClaimTypes.Name, user.UserName!)
					};

					var identity = new ClaimsIdentity(claims, "Custom");
					var principal = new ClaimsPrincipal(identity);

					await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme, principal);

					// Pass the claims to the GetToken method
					var content = new StringContent(JsonConvert.SerializeObject(claims), Encoding.UTF8, "application/json");
					var response = await _httpClient.PostAsync("https://localhost:7152/api/Auth/token?key=Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh", content);

					if (response.IsSuccessStatusCode)
					{
						var token = await response.Content.ReadAsStringAsync();
						// Store the token in a secure place, like a secure cookie or the session
						HttpContext.Session.SetString("JwtToken", token);
						return RedirectToAction("Details", "Account");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Failed to generate token.");
						return View(viewModel);
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(viewModel);
				}
			}
		}

		ViewData["StatusMessage"] = "Incorrect e-mail and password";
		return View(viewModel);
	}

	#endregion

	#region Sign Out

	[HttpGet]
    [Route("/signout")]
    
    public new async Task<IActionResult> SignOut()
    {
        Response.Cookies.Delete("AccessToken");
        await _signInManager.SignOutAsync();
        return RedirectToAction("Signin", "Auth");

    }



    //[Route("/auth/details")]
    //[HttpGet]
    //public async Task<IActionResult> Details()
    //{
    //    if (!_signInManager.IsSignedIn(User))
    //        return RedirectToAction("Details", "Account");

    //    var userEntity = await _userManager.GetUserAsync(User);

    //    var viewModel = new AccountDetailsViewModel();
    //    return View("~/Views/Account/Details.cshtml", viewModel);
    //}

    #endregion

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


