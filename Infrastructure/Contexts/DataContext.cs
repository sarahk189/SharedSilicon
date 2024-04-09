using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{

	public DbSet<AddressEntity> Addresses { get; set; } = null!;
	public DbSet<SubscribeEntity> Subscribe { get; set; } = null!;
	public DbSet<CourseEntity> Courses { get; set; } = null!;
	public DbSet<SavedCourseEntity> SavedCourses { get; set; } = null!;
	public DbSet<CourseDetailsEntity> CoursesDetails { get; set;} = null!;
	public DbSet<CourseAuthorEntity> CoursesAuthor { get; set;} = null!;
	public DbSet<ContactRequestEntity> ContactRequests { get; set; }
	public DbSet<CategoryEntity> Categories { get; set; } = null!;
	public DbSet<ContactRequestEntity> ContactRequests { get; set; } = null!;
	public DbSet<FilterCategoryEntity> FilterCategories { get; set; } = null!;
	public DbSet<ContactEntity> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<CourseEntity>()
            .HasOne(c => c.CourseDetails)
            .WithOne(cd => cd.Course)
            .HasForeignKey<CourseDetailsEntity>(cd => cd.CourseId);
    }

}
