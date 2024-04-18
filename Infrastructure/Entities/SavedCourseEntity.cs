using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[PrimaryKey("UserId", "CourseId")]
public class SavedCourseEntity
{
	//public int Id { get; set; }
	public string UserId { get; set; } = null!;

	[ForeignKey("UserId")]
	public UserEntity User { get; set; } = null!;

	public int CourseId { get; set; }

	[ForeignKey("CourseId")]
	public CourseEntity Course { get; set; } = null!;
}
