

namespace Infrastructure.Dtos;

public class CourseAuthorDto
{

	public string AuthorImageUrl { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string? Headline { get; set; }

}
