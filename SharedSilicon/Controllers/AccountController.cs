using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class AccountController : Controller
{
    //private readonly AccountService _accountService;

    //public AccountController(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressInfo();

        return View(viewModel); 
    }

    [Route("/security")]
    public IActionResult Security(SecurityViewModel viewModel)
    {
        //_securityService.ChangePassword(viewModel.Password);
        return View (viewModel);
    }


    [Route("/mycourses")]
    public IActionResult SavedCourses(SavedCoursesViewModel viewModel)
    {
        return View (viewModel);
    }

    //[HttpGet]
    //public IActionResult BasicInfo()
    //{

    //    return View();
    //}

    //[HttpPost]
    //public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
    //{
    //    //_accountService.SaveBasicInfo(viewModel.BasicInfo);
    //    return RedirectToAction(nameof(Details), viewModel);
    //}

    //[HttpGet]

    //public IActionResult AddressInfo()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
    //{
    //    //_accountService.SaveAddressInfo(viewModel.AddressInfo);
    //    return RedirectToAction(nameof(Details), viewModel);
    //}

}
