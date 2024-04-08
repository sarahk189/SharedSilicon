﻿using Infrastructure.Entities;
using Infrastructure.Dtos;
using SharedSilicon.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static SharedSilicon.Models.CoursesModel;


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

	[Route("Courses/Details/{id}")]
	[HttpGet("Details/{id}")]
	public async Task<IActionResult> CourseDetails(int id)
	{
		using var http = new HttpClient();
		var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var courseDto = JsonConvert.DeserializeObject<Infrastructure.Dtos.CourseDto>(json);

		if (courseDto == null)
		{
			return NotFound();
		}

		return View("Sections/_SingleCourse", courseDto);
	}
}
