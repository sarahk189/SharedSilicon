
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class FilterCategoryEntity
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int CourseId { get; set; }

    public ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

    public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();  
}
