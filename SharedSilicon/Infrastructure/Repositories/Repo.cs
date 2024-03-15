using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public abstract class Repo<TEntity>(DataContext context) where TEntity : class
{
	private readonly DataContext _context = context;

	public virtual async Task<TEntity> CreateAsync(TEntity entity)
	{
		try
		{
			_context.Set<TEntity>().Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		catch
		{

		}
		return null!;
	}

	public virtual async Task<TEntity> UpdateAsync(TEntity entity)
	{
		try
		{
			_context.Set<TEntity>().Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		catch
		{

		}
		return null!;
	}

	public virtual async Task<List<TEntity>> GetAllAsync()
	{
		try
		{
			var result = await _context.Set<TEntity>().ToListAsync();
			return result;
		}
		catch
		{

		}
		return null!;
	}

	public virtual async Task<TEntity> DeleteAsync(TEntity entity)
	{
		try
		{
			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		catch
		{

		}
		return null!;
	}
	
}

