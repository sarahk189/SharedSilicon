using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CoursesController(DataContext context) : ControllerBase
{


	#region CREATE
	[HttpPost]
	public async Task<IActionResult> Create(CreateCourseDto createCourseDto)
	{
		if (ModelState.IsValid)
		{
			if (!await context.Courses.AnyAsync(x => x.Title == createCourseDto.Course.Title))
			{
				var courseEntity = new CourseEntity
				{
					Title = createCourseDto.Course.Title,
					ImageUrl = createCourseDto.Course.ImageUrl,
					BestBadgeUrl = createCourseDto.Course.BestBadgeUrl,
					BookmarkUrl = createCourseDto.Course.BookmarkUrl,
					Hours = createCourseDto.Course.Hours,
					Price = createCourseDto.Course.Price,
					OldPrice = createCourseDto.Course.OldPrice,
					RedPrice = createCourseDto.Course.RedPrice,
					RatingPercentage = createCourseDto.Course.RatingPercentage,
					RatingCount = createCourseDto.Course.RatingCount,
					Author = new CourseAuthorEntity
					{
						AuthorImageUrl = createCourseDto.Author.AuthorImageUrl,
						FirstName = createCourseDto.Author.FirstName,
						LastName = createCourseDto.Author.LastName,
						Headline = createCourseDto.Author.Headline
					},
					FilterCategory = new List<FilterCategoryEntity>
					{
						new FilterCategoryEntity
						{
							Category = new CategoryEntity
							{
								Name = createCourseDto.CategoryName.Name
							}
						}
					}
				};

				await context.Courses.AddAsync(courseEntity);
				await context.SaveChangesAsync();

				var courseDetailsEntity = new CourseDetailsEntity
				{
					NumberOfReviews = createCourseDto.CourseDetails.NumberOfReviews,
					Digital = createCourseDto.CourseDetails.Digital,
					CourseId = courseEntity.Id
				};

				await context.CoursesDetails.AddAsync(courseDetailsEntity);
				await context.SaveChangesAsync();

				courseEntity.CourseDetailsId = courseDetailsEntity.Id;

				context.Courses.Update(courseEntity);
				await context.SaveChangesAsync();

				return Created("", null);
			}
			return Conflict();
		}
		return BadRequest();
	}


	#endregion region


	#region READ
	[HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await context.Courses.ToListAsync();
		var courseDtos = courses.Select(course => new CourseDto

		{ 
			Title = course.Title,
			ImageUrl = course.ImageUrl,
			BestBadgeUrl = course.BestBadgeUrl,
			BookmarkUrl = course.BookmarkUrl,
			Hours = course.Hours,
			Price = course.Price,
			OldPrice = course.OldPrice,
			RedPrice = course.RedPrice,
			RatingPercentage = course.RatingPercentage,
			RatingCount = course.RatingCount

		}).ToList();

		return Ok(courseDtos);
	}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
		var course = await context.Courses
	  .Include(c => c.CourseDetails)
	  .Include(c => c.Author)
	  .FirstOrDefaultAsync(x => x.Id == id);
		if (course != null)
		{
			var courseDetailsDto = new CourseDetailsDto
			{
				NumberOfReviews = course.CourseDetails.NumberOfReviews,
				Digital = course.CourseDetails.Digital,
			};
            
            var courseAuthorDto = new CourseAuthorDto
			{
				AuthorImageUrl = course.Author.AuthorImageUrl,
				FirstName = course.Author.FirstName,
				LastName = course.Author.LastName,
				Headline = course.Author.Headline
			};
            return Ok(new {CourseDetails = courseDetailsDto,CourseAuthor = courseAuthorDto });

        }

        return NotFound();
        
    }

    #endregion region


}
