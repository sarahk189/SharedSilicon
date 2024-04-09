using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; 
    
    public virtual ICollection <FilterCategoryEntity> FilterCategory { get; set; } = new List<FilterCategoryEntity>();

}
