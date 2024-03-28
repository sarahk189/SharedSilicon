using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static SharedSilicon.Models.CoursesModel;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{

    private readonly DataContext _context;

    public CoursesController(DataContext context)
    {
        _context = context;
    }


    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CourseEntity entity)
    {
        if (ModelState.IsValid)
        {
           if (!await _context.Courses.AnyAsync(x => x.Title == entity.Title))
            {
                               
                    var courseEntity = new CourseEntity
                    {
                        Id = entity.Id,
                        CourseDetailsId = entity.CourseDetailsId,
                        Title = entity.Title,
                        ImageUrl = entity.ImageUrl,
                        BestBadgeUrl = entity.BestBadgeUrl,
                        BookmarkUrl = entity.BookmarkUrl,
                        Hours = entity.Hours,
                        Price = entity.Price,
                        OldPrice = entity.OldPrice,
                        RedPrice = entity.RedPrice,
                        RatingPercentage = entity.RatingPercentage,
                        RatingCount = entity.RatingCount,
                        CourseDetails = entity.CourseDetails,
                        Authors = entity.Authors,
                        
                    };

                await _context.Courses.AddAsync(courseEntity);
                await _context.SaveChangesAsync();

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
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var courseEntity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (courseEntity != null)
        {
            return Ok(courseEntity);
        }

        return NotFound();
        
    }

    #endregion region


}
