using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;
using System.Net.Http.Headers;
using System.Net.WebSockets;



namespace SharedSilicon.Controllers;

public class CoursesController(CategoryService categoryService, CourseService courseService, IConfiguration configuration, HttpClient http) : Controller
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _http = http;

    public async Task<IActionResult> Index()
	{
		var categories = await _categoryService.GetCategoriesAsync();
		var courses = await _courseService.GetCoursesAsync();

		//var viewModel = new CoursesViewModel
		//{
		//	Categories = categories,
		//	Courses = courses
		//};

		//return View(viewModel);
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
        var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}?key={_configuration["ApiKey:Secret"]}");
        var json = await response.Content.ReadAsStringAsync();
        var courseDto = JsonConvert.DeserializeObject<Infrastructure.Dtos.CourseDto>(json);

        if (courseDto == null)
        {
            return NotFound();
        }
        return View();
    }
}
