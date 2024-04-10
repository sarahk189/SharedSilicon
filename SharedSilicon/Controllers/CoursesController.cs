using Infrastructure.Entities;
using Infrastructure.Dtos;
using SharedSilicon.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static SharedSilicon.Models.CoursesModel;
using Infrastructure.Services;
using SharedSilicon.ViewModels;


namespace SharedSilicon.Controllers;

public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{

	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;

	public async Task<IActionResult> Index()
	{
		var categories = await _categoryService.GetCategoriesAsync();
		var courses = await _courseService.GetCoursesAsync();

		var viewModel = new CoursesViewModel
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

    [Route("Courses/Details/{id}")]
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> CourseDetails(int id)
    {
        using var http = new HttpClient();
        var apiKey = "Yzg3OGM2MjAtZGRjYi00YzQ2LWI4M2YtY2M2Yzk2MmQyZWNh";
        var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}?key={apiKey}");
        var json = await response.Content.ReadAsStringAsync();
        var courseDto = JsonConvert.DeserializeObject<Infrastructure.Dtos.CourseDto>(json);

        if (courseDto == null)
        {
            return NotFound();
        }

        return View("Sections/_SingleCourse", courseDto);
    }
}
