using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using static SharedSilicon.Models.CoursesModel;



namespace SharedSilicon.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService, CourseService courseService, IConfiguration configuration, HttpClient http) : Controller
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _http = http;

	public async Task<IActionResult> Index()
	{
		if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _http.GetAsync($"https://localhost:7152/api/Courses?key={_configuration["ApiKey:Secret"]}");
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(data);

				var categories = await _categoryService.GetCategoriesAsync();

				var viewModel = new CourseIndexViewModel
				{
					Categories = categories,
					Courses = courses
				};

				return View(viewModel);
			}
		}
		return View();
	}
	//      var categories = await _categoryService.GetCategoriesAsync();
	//var courses = await _courseService.GetCoursesAsync();

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
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.GetAsync($"https://localhost:7152/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<Course>(data);
                return View(course);
            }
        }
        return View();
    }
}
