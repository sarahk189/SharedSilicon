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

	[HttpPost]
	public IActionResult Index(ContactViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		TempData["MessageSent"] = "Your message has been sent!";
		ViewData["Title"] = "Contact us";

		
		var viewModel = new ContactViewModel();

		ModelState.Clear();

		
		return View(viewModel);
	}
}
