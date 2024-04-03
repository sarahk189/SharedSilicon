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


						CourseDetails = new CourseDetailsEntity
						{
							NumberOfReviews = createCourseDto.CourseDetails.NumberOfReviews,
							Digital = createCourseDto.CourseDetails.Digital,
							CourseDescription = createCourseDto.CourseDetails.CourseDescription,
							WhatYoullLearn = createCourseDto.CourseDetails.WhatYoullLearn,
							NumberOfArticles = createCourseDto.CourseDetails.NumberOfArticles,
							NumberOfDownloads = createCourseDto.CourseDetails.NumberOfDownloads,
							Certificate = createCourseDto.CourseDetails.Certificate,
							ProgramDetailOne = createCourseDto.CourseDetails.ProgramDetailOne,
							ProgramDetailTwo = createCourseDto.CourseDetails.ProgramDetailTwo,
							ProgramDetailThree = createCourseDto.CourseDetails.ProgramDetailThree,
							ProgramDetailFour = createCourseDto.CourseDetails.ProgramDetailFour,
							ProgramDetailFive = createCourseDto.CourseDetails.ProgramDetailFive,

							Author = new CourseAuthorEntity
							{

								AuthorImageUrl = createCourseDto.Author.AuthorImageUrl,
								FirstName = createCourseDto.Author.FirstName,
								LastName = createCourseDto.Author.LastName,
								Headline = createCourseDto.Author.Headline,
								Description = createCourseDto.Author.Description,
								NumberOfSubscribers = createCourseDto.Author.NumberOfSubscribers,
								NumberOfFollowers = createCourseDto.Author.NumberOfFollowers,
								CourseId = createCourseDto.Course.Id,
							}
                        },
						
                        
                    };

                await context.Courses.AddAsync(courseEntity);
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
			Id = course.Id,
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
	  .Include(c => c.CourseDetails.Author)
	  .FirstOrDefaultAsync(x => x.Id == id);
		if (course != null)
		{
			var courseDetailsDto = new CourseDetailsDto
			{
				NumberOfReviews = course.CourseDetails.NumberOfReviews,
				Digital = course.CourseDetails.Digital,
				CourseDescription = course.CourseDetails.CourseDescription,
				WhatYoullLearn = course.CourseDetails.WhatYoullLearn,
				NumberOfArticles = course.CourseDetails.NumberOfArticles,
				NumberOfDownloads = course.CourseDetails.NumberOfDownloads,
				Certificate = course.CourseDetails.Certificate,
				ProgramDetailOne = course.CourseDetails.ProgramDetailOne,
				ProgramDetailTwo = course.CourseDetails.ProgramDetailTwo,
				ProgramDetailThree = course.CourseDetails.ProgramDetailThree,
				ProgramDetailFour = course.CourseDetails.ProgramDetailFour,
				ProgramDetailFive = course.CourseDetails.ProgramDetailFive
				
			};
			return Ok(courseDetailsDto);
		}

        return NotFound();
        
    }

    #endregion region


}
