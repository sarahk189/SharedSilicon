using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CourseDetailsEntity
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public decimal? NumberOfReviews { get; set; }
    public bool? Digital { get; set; }
    public string? CourseDescription {  get; set; }
    public string? WhatYoullLearn {  get; set; }
    public int? NumberOfArticles { get; set; }
    public int? NumberOfDownloads { get; set; }
    public bool? Certificate {  get; set; }
    public string? ProgramDetailOne { get; set;}
    public string? ProgramDetailTwo { get; set; }
    public string? ProgramDetailThree { get; set; }
    public string? ProgramDetailFour { get; set; }
    public string? ProgramDetailFive { get; set; }
    public virtual CourseEntity Course { get; set; } = null!;
    public virtual CourseAuthorEntity Author { get; set; } = null!;

}
