using Microsoft.AspNetCore.Mvc;
using SharedSilicon.ViewModels;

namespace SharedSilicon.Controllers;

public class ContactController : Controller
{
	public IActionResult Index()
	{
		ViewData["Title"] = "Contact us";

		var viewModel = new ContactViewModel();
		return View(viewModel);
	}
}
