using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using SharedSilicon.Models;

namespace SharedSilicon.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;

    public AccountController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    #region Details
    [Route("/account/details")]
    public async Task <IActionResult> Details()
    {
        var claims = HttpContext.User.Identities.FirstOrDefault();
        var viewModel = await PopulateViewModelAsync();
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressInfo();

        return View(viewModel);
    }

    public async Task<AccountDetailsViewModel> PopulateViewModelAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        
        try
        {
            if (user != null)
            {
                var address = user.Address;
                var viewModel = new AccountDetailsViewModel()
                {
                    BasicInfo = new AccountDetailsBasicInfoModel
                    {
                        ProfileImage = user.ProfileImage,
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

    [HttpGet]
    public IActionResult BasicInfo()
    {

        return View();
    }

    [HttpPost]
    public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveBasicInfo(viewModel.BasicInfo);
        return RedirectToAction(nameof(Details), viewModel);
    }

    [HttpGet]

    public IActionResult AddressInfo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveAddressInfo(viewModel.AddressInfo);
        return RedirectToAction(nameof(Details), viewModel);
    }

    #endregion

    #region Security
    [Route("/account/security")]
    public /*async Task<IActionResult>*/ IActionResult Security(SecurityViewModel viewModel)
    {

        //// Get the currently logged in user
        //var user = await _userManager.GetUserAsync(User);

        //if (user == null)
        //{
        //    // Handle case where user is not logged in
        //    return NotFound();
        //}

        //// Change the user's password
        //var result = await _userManager.ChangePasswordAsync(user,viewModel.CurrentPassword, viewModel.NewPassword);

        //if (!result.Succeeded)
        //{
        //    // Handle case where password change failed
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //    return View(viewModel);
        //}

        //// Handle case where password change succeeded
        //// ...

        return View(viewModel);
    }
    #endregion

    #region MyCourses
    [Route("/account/mycourses")]
    public IActionResult SavedCourses(SavedCoursesViewModel viewModel)
    {
        return View (viewModel);
    }
    #endregion


}
