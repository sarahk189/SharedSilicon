


namespace Infrastructure.Dtos;

public class CreateCourseDto
{
	public CourseDto Course { get; set; } = null!;
	public CourseDetailsDto CourseDetails { get; set; } = null!;
	public CourseAuthorDto Author { get; set; } = null!;
}
