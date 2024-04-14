namespace Infrastructure.Dtos;

public class CourseResult
{
	public bool Succeeded { get; set; }
	public IEnumerable<CourseDto>? Courses { get; set; }
}
