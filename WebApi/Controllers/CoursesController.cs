using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(DataContext context) : ControllerBase
{

	#region CREATE
	[Authorize]
	[HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto createCourseDto)
	{
		if (ModelState.IsValid)
		{
			if (!await context.Courses.AnyAsync(x => x.Title == createCourseDto.Course.Title))
			{
				var categoryEntity = await context.Categories
					.FirstOrDefaultAsync(c => c.Name == createCourseDto.CategoryName.Name);

				if (categoryEntity == null)
				{
					categoryEntity = new CategoryEntity
					{
						Name = createCourseDto.CategoryName.Name
					};

					await context.Categories.AddAsync(categoryEntity);
					await context.SaveChangesAsync();
				}

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
						AuthorImageUrl = createCourseDto.Course.Author.AuthorImageUrl,
						FirstName = createCourseDto.Course.Author.FirstName,
						LastName = createCourseDto.Course.Author.LastName,
						Headline = createCourseDto.Course.Author.Headline
					},

					FilterCategory = new List<FilterCategoryEntity>
				{
					new FilterCategoryEntity
					{
						Category = categoryEntity
					}
				}
				};

				await context.Courses.AddAsync(courseEntity);
				await context.SaveChangesAsync();

				var courseDetailsEntity = new CourseDetailsEntity
				{
					NumberOfReviews = createCourseDto.Course.CourseDetails.NumberOfReviews,
					Digital = createCourseDto.Course.CourseDetails.Digital,
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

	
	public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 3)
	{
		

		var query = context.Courses
			.Include(x => x.FilterCategory)
			.ThenInclude(x => x.Category)
			.AsQueryable();

		if (!string.IsNullOrWhiteSpace(category) && category != "all")
			query = query.Where(x => x.FilterCategory.Any(fc => fc.Category.Name == category));
		



		if (!string.IsNullOrEmpty(searchQuery))
			query = query.Where(x => x.Title.Contains(searchQuery) || 
			x.Author.FirstName.Contains(searchQuery)||
			x.Author.LastName.Contains(searchQuery));
		




		query = query.OrderBy(x => x.Id);



		var courses = await query.ToListAsync();
		

		var courseDtos = courses.Select(CourseFactory.Create).ToList();

		var response = new CourseResult
		{
			Succeeded = true,
			TotalItems = await query.CountAsync()
			
		};

		
		response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
		response.Courses = CourseFactory.Create(await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());

		return Ok(response);
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
			var courseDto = new CourseDto
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
				RatingCount = course.RatingCount,
				CourseDetails = new CourseDetailsDto
				{
					NumberOfReviews = course.CourseDetails.NumberOfReviews,
					Digital = course.CourseDetails.Digital
				},
				Author = new CourseAuthorDto
				{
					AuthorImageUrl = course.Author.AuthorImageUrl,
					FirstName = course.Author.FirstName,
					LastName = course.Author.LastName,
					Headline = course.Author.Headline
				}
			};

			return Ok(courseDto);
		}

		return NotFound();
	}

	#endregion region

	#region UPDATE
	[Authorize]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateOne(int id, CourseDto createCourseDto)
	{
		try
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await context.Courses
                .Include(c => c.CourseDetails)
                .Include(c => c.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (course != null)
            {
                course.Id = id;
                course.Title = createCourseDto.Title;
                course.ImageUrl = createCourseDto.ImageUrl;
                course.BestBadgeUrl = createCourseDto.BestBadgeUrl;
                course.BookmarkUrl = createCourseDto.BookmarkUrl;
                course.Hours = createCourseDto.Hours;
                course.Price = createCourseDto.Price;
                course.OldPrice = createCourseDto.OldPrice;
                course.RedPrice = createCourseDto.RedPrice;
                course.RatingPercentage = createCourseDto.RatingPercentage;
                course.RatingCount = createCourseDto.RatingCount;

                course.Author.AuthorImageUrl = createCourseDto.Author.AuthorImageUrl;
                course.Author.FirstName = createCourseDto.Author.FirstName;
                course.Author.LastName = createCourseDto.Author.LastName;
                course.Author.Headline = createCourseDto.Author.Headline;

                course.CourseDetails.NumberOfReviews = createCourseDto.CourseDetails.NumberOfReviews;
                course.CourseDetails.Digital = createCourseDto.CourseDetails.Digital;

                context.Courses.Update(course);
                await context.SaveChangesAsync();

                return Ok(course);
            }
        }
		catch (Exception)
		{

			throw;
		}
		

		return NotFound();
	}





	#endregion

	#region DELETE
	[Authorize]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOne(int id)
	{
		var course = await context.Courses
			.Include(c => c.CourseDetails)
			.Include(c => c.Author)
			.FirstOrDefaultAsync(x => x.Id == id);

		if (course != null)
		{
			context.Courses.Remove(course);
			await context.SaveChangesAsync();

			return Ok();
		}

		return NotFound();
	}
	#endregion

}



