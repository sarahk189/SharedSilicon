using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.ViewModels;

public class CourseRegistrationFormViewModel
{

    [Required]
    [Display(Name ="Title")]
    public string Title { get; set; } = null!;

    [Display(Name = "Price")]
    public string? Price { get; set; }

    [Display(Name = "Discount price")]
    public string? RedPrice { get; set; }

    [Display(Name = "Hours")]
    public int? Hours { get; set; }

    [Display(Name = "Is a bestseller")]
    public bool BestBadgeUrl { get; set; } = false;

    [Display(Name = "Likes in numbers")]
    public decimal? RatingCount { get; set; }

    [Display(Name = "Likes in procent")]
    public decimal? RatingPercentage { get; set; }

    [Display(Name = "Author(s)")]
    public virtual CourseAuthorEntity? Author { get; set; }
}
