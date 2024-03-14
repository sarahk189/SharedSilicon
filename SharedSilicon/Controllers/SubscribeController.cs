using SharedSilicon.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class SubscribeController : Controller
{
    [Route("subscribe")]
    [HttpGet]
    public IActionResult Subscribe()
    {
        var viewModel = new SubscribeViewModel();
        return View("~/Views/Shared/Sections/_Subscribe.cshtml", viewModel);
    }

    [Route("subscribe")]
    [HttpPost]
    public IActionResult Subscribe(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
            return RedirectToAction("Success");
        return View("~/Views/Shared/Sections/_Subscribe.cshtml", viewModel);
    }
   
}
