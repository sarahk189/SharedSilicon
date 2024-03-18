using Infrastructure.Contexts;
using Infrastructure.Dto;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repositories;

public abstract class Repo<TEntity>(DataContext context) where TEntity : class
{
	private readonly DataContext _context = context;

	public virtual async Task<ResponseResult> CreateAsync(TEntity entity)
	{
		try
		{
			_context.Set<TEntity>().Add(entity);
			await _context.SaveChangesAsync();
			return ResponseFactory.Ok(entity);
		}
		catch (Exception ex)
		{
			
			{
				return ResponseFactory.Error(ex.Message);
			};
		}
		
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

	public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
	{
		try
		{
			var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
			return result;
		}
		catch
		{

		}
		return null!;
	}

	public virtual async Task<IEnumerable<ResponseResult>> GetAllAsync()
	{
		try
		{
			IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
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

