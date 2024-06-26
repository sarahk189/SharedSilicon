﻿using Infrastructure.Contexts;
using Infrastructure.Dtos;
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

			IEnumerable<TEntity> entities = await _context.Set<TEntity>().ToListAsync();
			IEnumerable<ResponseResult> result = entities.Select(entity => ResponseFactory.Ok(entity));
			return result;
		}
		catch
		{

		}
		return [];
	}

	public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
	{
		try
		{
			if (await _context.Set<TEntity>().AnyAsync(predicate))
				return ResponseFactory.Exists();
			return ResponseFactory.NotFound();

		}
		catch (Exception ex)
		{

			return ResponseFactory.Error(ex.Message);
		}

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
