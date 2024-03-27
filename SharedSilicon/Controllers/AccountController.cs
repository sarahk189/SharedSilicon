using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using SharedSilicon.Models;
using System.Net;

namespace SharedSilicon.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    #region Details
    [HttpGet]
    [Route("/account/details")]
    public async Task <IActionResult> Details()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }
        var userEntity = await _userManager.GetUserAsync(User);
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
                var address = user.Address ?? new AddressEntity(); // If user.Address is null, create a new Address object

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
    
    [HttpPost]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Details", viewModel);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.FirstName = viewModel.BasicInfo.FirstName;
            user.LastName = viewModel.BasicInfo.LastName;
            user.Email = viewModel.BasicInfo.Email;
            user.PhoneNumber = viewModel.BasicInfo.Phone;
            user.Biography = viewModel.BasicInfo.Biography;

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
    //_accountService.SaveBasicInfo(viewModel.BasicInfo);


    [HttpPost]
    public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveAddressInfo(viewModel.AddressInfo);
        if (TryValidateModel(viewModel.AddressInfo))
        {
            return RedirectToAction(nameof(Details), viewModel);
        }
        return View("Details", viewModel);
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
