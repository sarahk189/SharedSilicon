using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    [Key]
    public int Id { get; set; }
    public int CourseDetailsId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageName { get; set; }
    public bool? IsBestSeller { get; set; }
    public int Hours { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal LikesInPercent { get; set; }
    public decimal LikesInNumbers { get; set; }
    public virtual CourseDetailsEntity CourseDetails { get; set; } = null!;
    public ICollection<CourseAuthorEntity> Authors { get; set; } = new List<CourseAuthorEntity>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
 }
