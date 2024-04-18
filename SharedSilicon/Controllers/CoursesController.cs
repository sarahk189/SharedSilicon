using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using SharedSilicon.ViewModels;




namespace SharedSilicon.Controllers;

public class CoursesController(CategoryService categoryService, CourseService courseService, IConfiguration configuration, HttpClient http, UserManager<UserEntity> userManager, SavedCoursesRepository savedCoursesRepository) : Controller
{
	private readonly CategoryService _categoryService = categoryService;
	private readonly CourseService _courseService = courseService;
	private readonly IConfiguration _configuration = configuration;
	private readonly HttpClient _http = http;
	private readonly UserManager<UserEntity> _userManager = userManager;
	private readonly SavedCoursesRepository _savedCoursesRepository = savedCoursesRepository;

	public async Task<IActionResult> Index(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 3)
	{

		var courseResult = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);



		var viewModel = new CourseIndexViewModel
		{
			Categories = await _categoryService.GetCategoriesAsync(),
			Courses = courseResult.Courses,
			Pagination = new Pagination
			{
				PageSize = pageSize,
				CurrentPage = pageNumber,
				TotalPages = courseResult.TotalPages,
				TotalItems = courseResult.TotalItems

			}
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
		return View(courseDto);
	}

	public async Task<IActionResult> Bookmark(int courseId)
	{
		var userEntity = await _userManager.GetUserAsync(User);
		var savedCourseEntity = new SavedCourseEntity
		{
			CourseId = courseId,
			UserId = userEntity.Id
		};

		if (userEntity != null)
		{
			var result = await _savedCoursesRepository.ExistsAsync(x => x.UserId == userEntity.Id && x.CourseId == courseId);
			if (result.StatusCode == Infrastructure.Dtos.StatusCode.EXISTS)
			{
				
				var deleteresult = await _savedCoursesRepository.DeleteAsync(savedCourseEntity);
					
			}
			else if (result.StatusCode == Infrastructure.Dtos.StatusCode.NOT_FOUND)
			{
				var saveresult = await _savedCoursesRepository.CreateAsync(savedCourseEntity);
			}
		}

		return RedirectToAction("Index");

	}



}
