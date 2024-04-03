
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CourseAuthorEntity
{
    [Key]
    public Guid Id { get; set; }
    public int CourseId { get; set; }
    public string AuthorImageUrl { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Headline { get; set; }
    public string? Description { get; set; }
    public int? NumberOfSubscribers { get; set; }
    public int? NumberOfFollowers { get; set; }

   
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];


}
