using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
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
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var courseEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (courseEntity != null)
        {
            return Ok(courseEntity);
        }

        return NotFound();
        
    }

    #endregion region


}
