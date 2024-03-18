using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext context) : Repo<UserEntity>(context)

{
	private readonly DataContext _context = context;

	public override async Task<IEnumerable<UserEntity>> GetAllAsync()
	{
		try
		{
			IEnumerable<UserEntity> result = await _context.Users
				.Include(i => i.Address)
				.ToListAsync();
			return result;
		}
		catch
		{

		}
		return null!;
	}

	public override async Task<UserEntity> GetOneAsync(Expression<Func<UserEntity, bool>> predicate)
	{
		try
		{
			var result = await _context.Set<UserEntity>()
				.Include(i => i.Address)
				.FirstOrDefaultAsync(predicate);
			return result;
		}
		catch
		{

		}
		return null!;
	}
}
