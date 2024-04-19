namespace Infrastructure.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; 
    
    public virtual ICollection <FilterCategoryEntity> FilterCategory { get; set; } = new List<FilterCategoryEntity>();

}
