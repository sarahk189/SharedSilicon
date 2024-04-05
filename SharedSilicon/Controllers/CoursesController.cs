using Infrastructure.Entities;
using Infrastructure.Dtos;
using SharedSilicon.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


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
	//[HttpGet("Details/{id}")]
	public async Task<IActionResult> CourseDetails(int id)
	{
		using var http = new HttpClient();
		var response = await http.GetAsync($"https://localhost:7152/api/courses/{id}");
		var json = await response.Content.ReadAsStringAsync();
		var entity = JsonConvert.DeserializeObject<CourseDetailsEntity>(json);

		var dto = new Infrastructure.Dtos.CourseDetailsDto
		{
			NumberOfReviews = entity.NumberOfReviews,
			Digital = entity.Digital
		};

		if (entity.Course == null)
		{
			return NotFound();
		}
		var courseDto = new Infrastructure.Dtos.CourseDto
		{
			Title = entity.Course.Title,
			ImageUrl = entity.Course.ImageUrl,
			BestBadgeUrl = entity.Course.BestBadgeUrl,
			BookmarkUrl = entity.Course.BookmarkUrl,
			Hours = entity.Course.Hours,
			Price = entity.Course.Price,
			OldPrice = entity.Course.OldPrice,
			RedPrice = entity.Course.RedPrice,
			RatingPercentage = entity.Course.RatingPercentage,
			RatingCount = entity.Course.RatingCount,
			CourseDetails = new Infrastructure.Dtos.CourseDetailsDto
			{
				NumberOfReviews = entity.Course.CourseDetails.NumberOfReviews,
				Digital = entity.Course.CourseDetails.Digital
			},
			Author = new Infrastructure.Dtos.CourseAuthorDto
			{
				AuthorImageUrl = entity.Course.Author.AuthorImageUrl,
				FirstName = entity.Course.Author.FirstName,
				LastName = entity.Course.Author.LastName,
				Headline = entity.Course.Author.Headline
			}
		};

		return View("CourseDetails", courseDto);
	}
}
