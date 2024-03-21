using ASPNETAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETAssignment.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			ViewData["Title"] = "Contact us";
			
			var viewModel = new ContactViewModel();
			return View(viewModel);		
			
		}

    }
}
