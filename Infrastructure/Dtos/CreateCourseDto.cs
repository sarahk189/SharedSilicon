namespace Infrastructure.Dtos;

public class CreateCourseDto
{
	
	public CourseDto Course { get; set; } = null!;
    public CategoryDto CategoryName { get; set; }  = null!;

	
}
