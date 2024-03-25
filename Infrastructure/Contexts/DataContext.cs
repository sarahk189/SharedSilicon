﻿using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<UserEntity> Users { get; set; } = null!;
	public DbSet<AddressEntity> Addresses { get; set; } = null!;
	public DbSet<SubscribeEntity> Subscribe { get; set; } = null!;
	public DbSet<CourseEntity> Courses { get; set; } = null!;
	public DbSet<CourseDetailsEntity> CoursesDetails { get; set;} = null!;
	public DbSet<CourseAuthorEntity> CoursesAuthor { get; set;} = null!;

}
