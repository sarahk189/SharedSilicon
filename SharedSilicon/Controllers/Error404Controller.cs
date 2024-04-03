using Microsoft.AspNetCore.Mvc;

namespace SharedSilicon.Controllers;

public class Error404Controller : Controller
{
	//kanske ta bort denna, behövde bara se så det fungerade
	[Route("/error/404")]
	public IActionResult Index()
	{
		ViewData["Title"] = "Ooops! Page was not found";
		return View();
	}
}
