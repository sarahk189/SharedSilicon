using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using SharedSilicon.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Infrastructure.Repositories;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace SharedSilicon.Controllers;
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, SavedCoursesRepository savedCoursesRepository, DataContext context) : Controller
{
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SignInManager<UserEntity> _signInManager = signInManager;
	private readonly SavedCoursesRepository _savedCoursesRepository = savedCoursesRepository;
	private readonly DataContext _context = context;


	#region Details
	//If signed in, directs to the account details page
	[HttpGet]
	[Route("/account/details")]
	public async Task<IActionResult> Details()
	{
		var userEntity = await _userManager.GetUserAsync(User);
		if (!_signInManager.IsSignedIn(User))
		{
			return RedirectToAction("SignIn", "Auth");
		}
		var claims = HttpContext.User.Identities.FirstOrDefault();
		var viewModel = await PopulateViewModelAsync();

		return View(viewModel);
	}


    public async Task<AccountDetailsViewModel> PopulateViewModelAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var address = _context.Addresses.FirstOrDefault(a => a.Id == user.AddressId);

        try
        {
            if (user != null)
            {
                address = user.Address ?? new AddressEntity();

                var viewModel = new AccountDetailsViewModel()
                {
                    BasicInfo = new BasicInfoFormViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email!,
                        Phone = user.PhoneNumber!,
                        Biography = user.Biography
                    },
					AddressInfo = new AddressInfoFormViewModel()
					{
						Addressline_1 = address.AddressLine1,
						Addressline_2 = address.AddressLine2,
						PostalCode = address.PostalCode,
						City = address.City
					}
                };

                if (address != null)
                {
                    viewModel.AddressInfo = new AddressInfoFormViewModel
                    {
                        Addressline_1 = address.AddressLine1,
                        Addressline_2 = address.AddressLine2,
                        PostalCode = address.PostalCode,
                        City = address.City
                    };
                }

                return viewModel;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

		return null!;
	}

    public async Task<AccountDetailsViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        try
        {
            if (user != null)
            {
                return new AccountDetailsViewModel
                {
                    BasicInfo = new BasicInfoFormViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email!,
                        Phone = user.PhoneNumber!,
                        Biography = user.Biography
                    }
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return null!;
    }

    public async Task<AccountDetailsViewModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var address = user.Address;

        try
        {
            if (user != null)
            {
                return new AccountDetailsViewModel
                {
					AddressInfo = new AddressInfoFormViewModel
                    {
                        Addressline_1 = address.AddressLine1,
                        Addressline_2 = address.AddressLine2,
                        PostalCode = address.PostalCode,
                        City = address.City
                    }
                };
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
		if (!TryValidateModel(viewModel.BasicInfo))
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
				await PopulateViewModelAsync();
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

			if (user.Address != null)
			{
				user.Address.AddressLine1 = viewModel.AddressInfo.Addressline_1;
				user.Address.AddressLine2 = viewModel.AddressInfo.Addressline_2!;
				user.Address.PostalCode = viewModel.AddressInfo.PostalCode;
				user.Address.City = viewModel.AddressInfo.City;

				var updated = await _userManager.UpdateAsync(user);
				if (updated.Succeeded)
				{
					await PopulateViewModelAsync();
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
					viewModel = await PopulateViewModelAsync();
					return View("Details", viewModel);
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
				FirstName = userEntity.FirstName,
				LastName = userEntity.LastName,
				Email = userEntity.Email,
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

		// Check if the current password or new password is null
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
	public async Task<IActionResult> SavedCourses()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user != null)
		{
			var savedCoursesEntities = await _savedCoursesRepository.GetSavedCoursesAsync(user.Id);

			var savedCourses = savedCoursesEntities.Select(savedCourseEntity => new SavedCourseDto
			{
				User = new UserDto
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email!
				},
				Course = PopulateCourseDto(savedCourseEntity.Course)

                
            });

			var viewModel = new SavedCoursesIndexViewModel
			{
				SavedCourses = savedCourses,
				User = new UserDto
				{
					UserId = Guid.Parse(user.Id),
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email!
				}
			};

			return View(viewModel);
		}

		return RedirectToAction("Index");
	}


	public CourseDto PopulateCourseDto(CourseEntity courseEntity)
	{
		if (courseEntity == null || courseEntity.Author == null)
		{
			return null;
		}

		return new CourseDto
		{
			Id = courseEntity.Id,
			Title = courseEntity.Title,
			ImageUrl = courseEntity.ImageUrl,
			BestBadgeUrl = courseEntity.BestBadgeUrl,
			BookmarkUrl = courseEntity.BookmarkUrl,
			Hours = courseEntity.Hours,
			Price = courseEntity.Price,
			OldPrice = courseEntity.OldPrice,
			RedPrice = courseEntity.RedPrice,
			RatingCount = courseEntity.RatingCount,
			Author = new CourseAuthorDto
			{
				FirstName = courseEntity.Author.FirstName,
				LastName = courseEntity.Author.LastName,
                AuthorImageUrl = courseEntity.Author.AuthorImageUrl,
                Headline = courseEntity.Author.Headline
            }
		};
	}

	[HttpPost]
	public async Task<IActionResult> DeleteOneCourse(int courseId, string userId)
	{
		//Get the saved courses from the user thats logged in
		var savedCourses = await _savedCoursesRepository.GetSavedCoursesAsync(userId);

		//find the saved course that we want to delete
		var savedCourse = savedCourses.FirstOrDefault(sc => sc.CourseId == courseId);

		if (savedCourse != null)
		{
			// delete the course
			await _savedCoursesRepository.DeleteAsync(savedCourse);

			// get the updated list of saved courses
			var updatedSavedCourses = await _savedCoursesRepository.GetSavedCoursesAsync(userId);

			// make the new list of saved courses into Dtos
			var savedCoursesDtos = updatedSavedCourses.Select(sc => new SavedCourseDto
			{
				User = new UserDto
				{
					FirstName = sc.User.FirstName,
					LastName = sc.User.LastName,
					Email = sc.User.Email
				},
				Course = PopulateCourseDto(sc.Course)
			});

			var user = await _userManager.FindByIdAsync(userId);

			//get the viewmodel and return it
			var viewModel = new SavedCoursesIndexViewModel
			{
				SavedCourses = savedCoursesDtos,
				User = new UserDto
				{
					UserId = Guid.Parse(userId),
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email
				}
			};
			return View("SavedCourses", viewModel);
		}

		return NotFound();
	}



	[HttpPost]
	public async Task<IActionResult> DeleteAllSavedCourses(string userId)
	{
		//find all the saved courses connected to the user
		var savedCourses = await _savedCoursesRepository.GetSavedCoursesAsync(userId);

		//delete all the saved courses
		foreach (var savedCourse in savedCourses)
		{
			await _savedCoursesRepository.DeleteAsync(savedCourse);
		}

		//Go back to the view
		return RedirectToAction("SavedCourses");
	}
	#endregion
}