using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;


namespace SharedSilicon.Controllers;

public class CoursesController : Controller
{
    public async Task <IActionResult> Index()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7152/api/courses");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);
            return View(data);
        }
        else
        {
            // Log the status code and the reason for failure
            Console.WriteLine($"Request failed with status code: {response.StatusCode}, reason: {response.ReasonPhrase}");
            return View("Error");
        }

        //ViewData["Title"] = "Courses";

        //var viewModel = new CoursesViewModel();
        //return View(viewModel);
    }
    [Route("Courses/Details/{id}")]
	//[HttpGet("Details/{id}")]
	public async Task<IActionResult> CourseDetails(int id)
	{
		using var http = new HttpClient();
		var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<CourseDetailsDto>(json);

		return View("CourseDetails",data);
	}
}
