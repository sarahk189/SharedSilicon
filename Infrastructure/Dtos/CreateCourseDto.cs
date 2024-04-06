


using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class CreateCourseDto
{
	
	public CourseDto Course { get; set; } = null!;
    public CategoryDto CategoryName { get; set; }  = null!;

	//public CourseDetailsDto CourseDetails { get; set; } = null!;
	//public CourseAuthorDto CourseAuthor { get; set; } = null!;
}
