using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories;

public class CourseFactory
{


	public static CourseDto Create(CourseEntity entity)
	{
		try
		{
			return new CourseDto
			{
				Id = entity.Id,
				Title = entity.Title,
				ImageUrl = entity.ImageUrl,
				BestBadgeUrl = entity.BestBadgeUrl,
				BookmarkUrl = entity.BookmarkUrl,
				Hours = entity.Hours,
				Price = entity.Price,
				OldPrice = entity.OldPrice,
				RedPrice = entity.RedPrice,
				RatingPercentage = entity.RatingPercentage,
				RatingCount = entity.RatingCount,
				Category = entity.FilterCategory?.FirstOrDefault()?.Category?.Name ?? "Default Category",


				CourseDetails = entity.CourseDetails != null ? new CourseDetailsDto
				{
					NumberOfReviews = entity.CourseDetails.NumberOfReviews,
					Digital = entity.CourseDetails.Digital
				} : new CourseDetailsDto(),

				Author = entity.Author != null ? new CourseAuthorDto
				{
					AuthorImageUrl = entity.Author.AuthorImageUrl,
					FirstName = entity.Author.FirstName,
					LastName = entity.Author.LastName,
					Headline = entity.Author.Headline
				} : new CourseAuthorDto()


			};

		}
		catch
		{

		}
		return new CourseDto();
	}

	public static IEnumerable<CourseDto> Create(List<CourseEntity> entities)
	{
		var courses = new List<CourseDto>();
		try
		{

			foreach (var entity in entities)
				courses.Add(Create(entity));

		}
		catch { }
		return courses;
	}
}
