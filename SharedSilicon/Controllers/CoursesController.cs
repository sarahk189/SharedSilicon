using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SharedSilicon.ViewModels;



namespace SharedSilicon.Controllers;

public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;

	public async Task <IActionResult> Index(string category = "", string searchQuery="")
	{
		var categories = await _categoryService.GetCategoriesAsync();
		var courses = await _courseService.GetCoursesAsync(category, searchQuery);
		var viewModel = new CourseIndexViewModel
		{
			Categories = categories,
			Courses = courses
		};
		return View(viewModel);
	}






	//public async Task <IActionResult> Index()
	//   {
	//       using var http = new HttpClient();
	//       var response = await http.GetAsync("https://localhost:7152/api/courses");       
	//       var json = await response.Content.ReadAsStringAsync();
	//       var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);


	//       return View(data);


	//   }




	//[Route("Courses/Details/{id}")]
	//[HttpGet("Details/{id}")]
	//public async Task<IActionResult> CourseDetails(int id)
	//{
	//    try
	//    {
	//        var courseDto = await _courseService.GetCourseAsync(id);
	//        return View("Sections/_SingleCourse", courseDto);
	//    }
	//    catch (Exception)
	//    {

	//        return RedirectToAction("Error", "Home");
	//    }
	//}




	//[Route("Courses/Details/{id}")]
	//[HttpGet("Details/{id}")]
	//public async Task<IActionResult> CourseDetails(int id)
	//{
	//	using var http = new HttpClient();
	//	var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}");
	//	var json = await response.Content.ReadAsStringAsync();
	//	var courseDto = JsonConvert.DeserializeObject<Infrastructure.Dtos.CourseDto>(json);

	//	if (courseDto == null)
	//	{
	//		return NotFound();
	//	}

	//	return View("Sections/_SingleCourse", courseDto);
	//}


	



}
