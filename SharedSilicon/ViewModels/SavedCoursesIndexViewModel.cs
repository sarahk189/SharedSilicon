﻿using Infrastructure.Dtos;

namespace SharedSilicon.ViewModels;

public class SavedCoursesIndexViewModel
{
	public IEnumerable<SavedCourseDto>? SavedCourses { get; set; }
	public IEnumerable<CourseDto>? Courses { get; set; }
	public UserDto User { get; set; } = null!;
	
}
