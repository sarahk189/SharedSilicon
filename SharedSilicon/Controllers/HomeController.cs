using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        var viewModel = new SubscribeViewModel();
        return View(viewModel);
    }


    [HttpPost]
    public IActionResult Subscribe(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
            return RedirectToAction("Success");
        return View("~/Views/Shared/Sections/_Subscribe.cshtml", viewModel);
    }
}
 
