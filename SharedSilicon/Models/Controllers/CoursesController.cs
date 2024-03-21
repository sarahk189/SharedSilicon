using ASPNETAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace ASPNETAssignment.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
		{
            
			ViewData["Title"] = "Courses";
		
			var viewModel = new CoursesViewModel();
            return View(viewModel); 
           
        }
    }
}
