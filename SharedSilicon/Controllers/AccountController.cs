using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using SharedSilicon.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace SharedSilicon.Controllers;


public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, HttpClient http, IConfiguration configuration, ILogger<AccountController> logger) : Controller
{
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;
	private readonly HttpClient _http = http;
	private readonly IConfiguration _configuration = configuration;
	private readonly ILogger<AccountController> _logger = logger;

	#region Details
	[HttpGet]
	[Route("/account/details")]
	public async Task<IActionResult> Details()
	{
		// Retrieve the token from the session
		var token = HttpContext.Session.GetString("JwtToken");
		if (string.IsNullOrEmpty(token))
		{
			// If the token is not in the session, redirect the user to the sign in page
			return RedirectToAction("SignIn", "Auth");
		}

		// Use the token to make authenticated requests...
		// For example, you can set the Authorization header of your HttpClient
		_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		// Extract the user's ID or username from the token
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(token);
		var claims = jwtToken.Claims;
		var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
		if (userIdClaim == null)
		{
			// Handle the case where the claim is not found
			return NotFound("User ID claim not found in the token");
		}
		var userId = userIdClaim.Value;

		// Get the user based on the user's ID or username
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null)
		{
			// Handle the case where the user is not found
			return NotFound();
		}

		// Populate the view model based on the user
		var viewModel = await PopulateViewModelAsync(user);
		return View(viewModel);
	}


	public async Task<AccountDetailsViewModel> PopulateViewModelAsync(UserEntity user)
	{
        var viewModel = new AccountDetailsViewModel();

        try
        {

            if (user != null)
            {
                var address = user.Address ?? new AddressEntity();

                viewModel.IsExternalAccount = user.IsExternalAccount;
                viewModel.BasicInfo = new AccountDetailsBasicInfoModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Phone = user.PhoneNumber!,
                    Biography = user.Biography
                };
                viewModel.AddressInfo = new AccountDetailsAddressInfoModel
                {
                    Addressline_1 = address.AddressLine1,
                    Addressline_2 = address.AddressLine2!,
                    PostalCode = address.PostalCode,
                    City = address.City
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return viewModel;
    }

    [HttpPost]
	public async Task<IActionResult> SaveBasicInfo(AccountDetailsViewModel viewModel)
	{
		if (!TryValidateModel(viewModel.BasicInfo, nameof(viewModel.BasicInfo)))
		{
			var token = HttpContext.Session.GetString("JwtToken");
			if (string.IsNullOrEmpty(token))
			{
				// If the token is not in the session, redirect the user to the sign in page
				return RedirectToAction("SignIn", "Auth");
			}

			// Use the token to make authenticated requests...
			// For example, you can set the Authorization header of your HttpClient
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			// Extract the user's ID or username from the token
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);
			var claims = jwtToken.Claims;
			var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
			if (userIdClaim == null)
			{
				// Handle the case where the claim is not found
				return NotFound("User ID claim not found in the token");
			}
			var userId = userIdClaim.Value;
			var user = await _userManager.FindByIdAsync(userId);

			if (user != null)
			{
				if (viewModel.BasicInfo != null)
				{
					user.FirstName = viewModel.BasicInfo.FirstName;
					user.LastName = viewModel.BasicInfo.LastName;
					user.Email = viewModel.BasicInfo.Email;
					user.PhoneNumber = viewModel.BasicInfo.Phone!;
					user.Biography = viewModel.BasicInfo.Biography!;
				}



			}
			var result = await _userManager.UpdateAsync(user!);


			if (result.Succeeded)
			{
				return RedirectToAction(nameof(Details));

			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}
		return View("Details", viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> SaveAddressInfo(AccountDetailsViewModel viewModel)
	{
		if (!TryValidateModel(viewModel.AddressInfo, nameof(viewModel.AddressInfo)))
		{
			var user = await _userManager.GetUserAsync(User);

			if (user!.Address != null)
			{
				user.Address.AddressLine1 = viewModel.AddressInfo.Addressline_1;
				user.Address.AddressLine2 = viewModel.AddressInfo.Addressline_2!;
				user.Address.PostalCode = viewModel.AddressInfo.PostalCode;
				user.Address.City = viewModel.AddressInfo.City;

				var updated = await _userManager.UpdateAsync(user);
				if (updated.Succeeded)
				{
					return RedirectToAction(nameof(Details));
				}
				else
				{
					foreach (var error in updated.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			else
			{
				user.Address = new AddressEntity()
				{
					AddressLine1 = viewModel.AddressInfo.Addressline_1,
					AddressLine2 = viewModel.AddressInfo.Addressline_2!,
					PostalCode = viewModel.AddressInfo.PostalCode,
					City = viewModel.AddressInfo.City
				};
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Details));
				}
			}
		}
		return View("Details", "Account");
	}

	#endregion

	#region Security
	[HttpGet]
	[Route("/account/security")]
	public async Task<IActionResult> Security()
	{
		//if (!_signInManager.IsSignedIn(User))
		//{
		//	return RedirectToAction("SignIn", "Auth");
		//}
		var userEntity = await _userManager.GetUserAsync(User);
		var claims = HttpContext.User.Identities.FirstOrDefault();

		var viewModel = new SecurityViewModel
		{
			Password = new ChangePasswordModel
			{
				FirstName = userEntity!.FirstName,
				LastName = userEntity.LastName,
				Email = userEntity!.Email!,
				ProfileImage = userEntity.ProfileImage
			}
		};

		return View(viewModel);
	}



	[HttpPost]
	[Route("/account/security")]
	public async Task<IActionResult> Security(SecurityViewModel viewModel)
	{
		var user = await _userManager.GetUserAsync(User);

		if (user == null)
		{
			return NotFound();
		}

		if (string.IsNullOrEmpty(viewModel.Password.CurrentPassword) || string.IsNullOrEmpty(viewModel.Password.NewPassword))
		{
			ModelState.AddModelError(string.Empty, "Password cannot be null or empty");
			return View(viewModel);
		}

		var result = await _userManager.ChangePasswordAsync(user, viewModel.Password.CurrentPassword, viewModel.Password.NewPassword);

		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(viewModel);
		}
		return View(viewModel);
	}

	[HttpPost]
	[Route("/account/delete")]
	public async Task<IActionResult> DeleteAccount(SecurityViewModel viewModel)
	{
		if (viewModel.DeleteAccount)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.DeleteAsync(user);

			if (result.Succeeded)
			{
				await _signInManager.SignOutAsync();
				return RedirectToAction("SignUp", "Auth");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}
		return View("Security", viewModel);
	}
	#endregion

	#region MyCourses
	[HttpGet]
	[Route("/account/mycourses")]
	public async Task<IActionResult> SavedCourses(SavedCoursesViewModel viewModel)
	{
		if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _http.GetAsync($"https://localhost:7152/account/details?key={_configuration["ApiKey:Secret"]}");
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var courses = JsonConvert.DeserializeObject<List<SavedCourseEntity>>(data);
			}

		}
		return View();
	}
	#endregion
}
