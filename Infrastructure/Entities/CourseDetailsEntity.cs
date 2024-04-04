using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CourseDetailsEntity
{
    [Key]
    public Guid Id { get; set; }
   
    public int CourseId { get; set; }
    public decimal? NumberOfReviews { get; set; }
    public bool? Digital { get; set; }
	public virtual ICollection <CourseEntity> Courses { get; set; } = new List<CourseEntity>();
	public virtual CourseAuthorEntity Author { get; set; } = null!;
    
}
