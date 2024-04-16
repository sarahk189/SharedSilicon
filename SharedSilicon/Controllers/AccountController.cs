using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using SharedSilicon.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using static SharedSilicon.Models.SavedCoursesModel;
using Infrastructure.Contexts;
using Newtonsoft.Json;
using static SharedSilicon.Models.CoursesModel;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;

namespace SharedSilicon.Controllers;


public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, DataContext context, IConfiguration configuration, HttpClient http) : Controller
{
	
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;
	private readonly HttpClient _http = http;
	private readonly IConfiguration _configuration = configuration;

	#region Details
	[HttpGet]
	[Route("/account/details")]
	public async Task<IActionResult> Details()
	{

		var userEntity = await _userManager.GetUserAsync(User);
		if (userEntity == null)
		{
			return RedirectToAction("SignIn", "Auth");
		}

		var viewModel = await PopulateViewModelAsync();
		return View("Details", viewModel);
	}


	public async Task<AccountDetailsViewModel> PopulateViewModelAsync()
	{
		var user = await _userManager.GetUserAsync(User);

		try
		{
			if (user != null)
			{
				var address = user.Address ?? new AddressEntity();

				var viewModel = new AccountDetailsViewModel()
				{
					IsExternalAccount = user.IsExternalAccount,
					BasicInfo = new AccountDetailsBasicInfoModel
					{
						FirstName = user.FirstName,
						LastName = user.LastName,
						Email = user.Email!,
						Phone = user.PhoneNumber!,
						Biography = user.Biography						
					},
					AddressInfo = new AccountDetailsAddressInfoModel
					{
						Addressline_1 = address.AddressLine1,
						Addressline_2 = address.AddressLine2!,
						PostalCode = address.PostalCode,
						City = address.City
					}
				};
				return viewModel;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		return null!;
	}

	[HttpPost]
	public async Task<IActionResult> SaveBasicInfo(AccountDetailsViewModel viewModel)
	{
		if (!TryValidateModel(viewModel.BasicInfo, nameof(viewModel.BasicInfo)))
		{
			var user = await _userManager.GetUserAsync(User);

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
			var result = await _userManager.UpdateAsync(user);


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
				user.Address.PostalCode = viewModel.AddressInfo.City;

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
		if (!_signInManager.IsSignedIn(User))
		{
			return RedirectToAction("SignIn", "Auth");
		}
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
	public IActionResult SavedCourses(SavedCoursesViewModel viewModel)
	{
		return View(viewModel);
	}
	#endregion
}
