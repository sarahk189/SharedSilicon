using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseAuthorEntity
{
    [Key]
    public int Id { get; set; }
    public string AuthorImageUrl { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Headline { get; set; }

    public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();
}
