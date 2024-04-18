using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SavedCoursesRepository(DataContext context) : Repo<SavedCourseEntity>(context)
{

	private readonly DataContext _context = context;

    public async Task<IEnumerable<SavedCourseEntity>> GetSavedCoursesAsync(string userId)
    {
        return await _context.SavedCourses
			.Where(sc => sc.UserId == userId)
			.Include(sc => sc.User)
			.Include(sc => sc.Course)
			.Include(sc => sc.Course.Author)
			.ToListAsync();
	}
}
