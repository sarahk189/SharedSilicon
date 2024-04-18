namespace Infrastructure.Dtos;

public class SavedCourseDto
{
	public UserDto User { get; set; } = null!;
	public  CourseDto? Course { get; set; }
}
