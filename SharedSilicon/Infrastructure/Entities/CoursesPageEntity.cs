

namespace Infrastructure.Entities;

public class CoursesPageEntity
{
	public string Title { get; set; } = null!;
	public string Author { get; set; } = null!;
	public string ImageUrl { get; set; } = null!;
	public decimal? Price { get; set; }
	public decimal? RedPrice { get; set; }
	public decimal? OldPrice { get; set; }
	public int Hours { get; set; }
	public int RatingPercentage { get; set; }
	public string RatingCount { get; set; } = null!;

}
