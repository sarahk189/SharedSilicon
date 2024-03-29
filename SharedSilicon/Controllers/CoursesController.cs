using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;

namespace SharedSilicon.Controllers;

public class CoursesController : Controller
{
    public async Task <IActionResult> Index()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7152/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

        return View(data);

        //ViewData["Title"] = "Courses";

        //var viewModel = new CoursesViewModel();
        //return View(viewModel);
    }

	[HttpGet("{id}")]
	public async Task<IActionResult> Details(int id)
	{
		using var http = new HttpClient();
		var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<CourseDetailsEntity>(json);

		return View("_SingleCourse", data);
	}
}
