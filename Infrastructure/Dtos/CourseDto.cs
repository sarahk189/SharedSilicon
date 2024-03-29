

namespace Infrastructure.Dtos;

public class CourseDto
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public bool BestBadgeUrl { get; set; }
	public bool BookmarkUrl { get; set; }
	public int Hours { get; set; }
	public decimal? Price { get; set; }
	public decimal? OldPrice { get; set; }
	public decimal? RedPrice { get; set; }
	public decimal? RatingPercentage { get; set; }
	public decimal? RatingCount { get; set; }
}
