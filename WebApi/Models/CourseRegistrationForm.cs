using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CourseRegistrationForm
{

    [Required]
    public string Title { get; set; } = null!;
    public string? Price { get; set; }
    public string? RedPrice { get; set; }
    public int? Hours { get; set; }
    public bool BestBadgeUrl { get; set; } = false;
    public decimal? RatingCount { get; set; }
    public decimal? RatingPercentage { get; set; }
    public virtual CourseAuthorEntity? Author {get; set;}



}
