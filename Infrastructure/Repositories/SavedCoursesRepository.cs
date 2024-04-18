using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SavedCoursesRepository(DataContext context) : Repo<SavedCourseEntity>(context)
{

	private readonly DataContext _context = context; 
	

}
