using Microsoft.AspNetCore.Mvc;
using SharedSilicon.ViewModels;

namespace SharedSilicon.Controllers
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
