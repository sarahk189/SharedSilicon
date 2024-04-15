using Infrastructure.Dtos;

namespace SharedSilicon.ViewModels;

public class CourseIndexViewModel
{

	public IEnumerable<CategoryDto>? Categories { get; set; }
	public IEnumerable<CourseDto>? Courses { get; set; }

	public Pagination? Pagination { get; set; }
}
