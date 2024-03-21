using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class Error404Controller : Controller
{
	public IActionResult Index()
	{
		ViewData["Title"] = "Ooops! Page was not found";
		return View();
	}
}
