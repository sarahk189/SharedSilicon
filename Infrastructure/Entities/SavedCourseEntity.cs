

namespace Infrastructure.Entities;

public class SavedCourseEntity
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public UserEntity User { get; set; }

	public int CourseId { get; set; }
	public CourseEntity Course { get; set; }
}
