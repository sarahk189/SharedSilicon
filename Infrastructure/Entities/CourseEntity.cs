using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    [Key]
    public int Id { get; set; }
    //public int CourseDetailsId { get; set; }
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
    public virtual CourseDetailsEntity CourseDetails { get; set; } = null!;
    public ICollection<CourseAuthorEntity> Authors { get; set; } = new List<CourseAuthorEntity>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
 }