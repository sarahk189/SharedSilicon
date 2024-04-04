using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CourseDetailsEntity
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }
    public virtual CourseEntity Course { get; set; }

    public decimal? NumberOfReviews { get; set; }
    public bool? Digital { get; set; }
    
}
