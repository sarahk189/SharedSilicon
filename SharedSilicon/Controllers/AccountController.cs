using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;

namespace SharedSilicon.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;

    public AccountController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressInfo();

        return View(viewModel);
    }

    [Route("/security")]
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


    [Route("/mycourses")]
    public IActionResult SavedCourses(SavedCoursesViewModel viewModel)
    {
        return View (viewModel);
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

}
