using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    [Key]
    public int Id { get; set; }
    public int? CourseDetailsId { get; set; }
    public virtual CourseDetailsEntity CourseDetails { get; set; }
    public int AuthorId { get; set; } 
	public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public bool BestBadgeUrl { get; set; }
    public bool BookmarkUrl {  get; set; } 
    public int Hours { get; set; }
    public decimal? Price { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? RedPrice { get; set; }
    public decimal? RatingPercentage { get; set; }
    public decimal? RatingCount { get; set; }
	public virtual CourseAuthorEntity Author { get; set; }

    public virtual ICollection<FilterCategoryEntity> FilterCategory { get; set; } = new List<FilterCategoryEntity>();
    public virtual ICollection<SavedCourseEntity> SavedCourses { get; set; } = new List<SavedCourseEntity>();
}