﻿using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SavedCoursesController(UserManager<UserEntity> userManager, DataContext context) : ControllerBase
{

	#region READ
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CourseEntity>>> GetSavedCourses()
	{
		var user = await userManager.GetUserAsync(User);
		var userId = user.Id;
		var savedCourses = await context.SavedCourses
			.Where(x => x.UserId == userId)
			.Select(x => x.Course)
			.ToListAsync();

		return savedCourses;
	}

	#endregion

	#region CREATE
	[HttpPost]
	public async Task<ActionResult> AddCourseToSavedCourses([FromBody] SavedCourseDto savedCourseDto)
	{
		var user = await userManager.GetUserAsync(User);
		var userId = user!.Id;
		var courseId = savedCourseDto.Course!.Id;
		var course = await context.Courses.FindAsync(courseId);

		if (course == null)
		{
			return NotFound();
		}

		var savedCourse = new SavedCourseEntity
		{
			UserId = userId,
			CourseId = courseId
		};

		context.SavedCourses.Add(savedCourse);
		await context.SaveChangesAsync();

		return NoContent();
	}
	#endregion

	#region DELETE

	[HttpDelete("{courseId}")]
	public async Task<ActionResult> RemoveCourseFromSavedCourses(int courseId)
	{
		var user = await userManager.GetUserAsync(User);
		var userId = user.Id;
		var savedCourse = await context.SavedCourses
			.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);

		if (savedCourse == null)
		{
			return NotFound();
		}

		context.SavedCourses.Remove(savedCourse);
		await context.SaveChangesAsync();

		return NoContent();
	}

	#endregion
}
