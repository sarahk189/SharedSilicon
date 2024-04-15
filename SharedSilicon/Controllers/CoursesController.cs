using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;




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

        var viewModel = new CourseIndexViewModel
        {
            Categories = categories,
            Courses = courses
        };

        return View(viewModel);
    }
	

    [Route("Courses/Details/{id}")]
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> CourseDetails(int id)
    {
        using var http = new HttpClient();
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
