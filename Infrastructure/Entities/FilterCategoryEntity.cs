namespace Infrastructure.Entities;

public class FilterCategoryEntity
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int CourseId { get; set; }

    public virtual CategoryEntity Category { get; set; }
    public virtual CourseEntity Course { get; set; }

}
