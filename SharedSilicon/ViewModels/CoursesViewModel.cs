using Infrastructure.Dtos;
using static SharedSilicon.Models.CoursesModel;

namespace SharedSilicon.ViewModels;

public class CoursesViewModel
{

	public IEnumerable<CategoryDto> Categories { get; set; }
	public IEnumerable<CourseDto> Courses { get; set; }

	//public List<Course> Courses { get; set; } = new List<Course>
 //       {
 //           new() {
 //               Title = "Fullstack Web Developer Course from Scratch",
 //               Author = "By Robert Fox",
 //               ImageUrl =  "images/coursesImages/laptop.svg",
 //               BestBadgeUrl = "images/coursesImages/bestsellerbadge.svg",
 //               BookmarkUrl = "images/coursesImages/bookmark.svg",
 //               Price = 12.50m,
 //               Hours = 220,
 //               RatingPercentage = 94,
 //               RatingCount = "4.2K"
 //           },

 //            new() {
 //               Title = "HTML, CSS, JavaScript Web Developer",
 //               Author = "By Jenny Wilson & Marvin McKinney",
 //               ImageUrl = "images/coursesImages/mandesk.svg",
 //               BookmarkUrl = "images/coursesImages/bookmark.svg",
 //               Price = 15.99m,
 //               Hours = 160,
 //               RatingPercentage = 92,
 //               RatingCount = "3.1K"
 //           },

 //             new() {
 //                 Title = "The Complete Front-End Web Development Course",
 //                 Author = "By Albert Flores",
 //                 ImageUrl = "images/coursesImages/womanyellow.svg",
 //                 BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                 RedPrice = 9.99m,
 //                 OldPrice = 44.99m,
 //                 Hours = 100,
 //                 RatingPercentage = 98,
 //                 RatingCount = "2.7K"
 //             },

 //              new() {
 //                  Title = "iOS & Swift - The Complete iOS App Development Course",
 //                  Author = "By Marvin McKinney",
 //                  ImageUrl = "images/coursesImages/womancurls.svg",
 //                  BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                  Price = 15.99m,
 //                  Hours = 160,
 //                  RatingPercentage = 92,
 //                  RatingCount = "3.1K"
 //              },

 //               new() {
 //                   Title = "Data Science & Machine Learning with Python",
 //                   Author = "By Esther Howard",
 //                   ImageUrl = "images/coursesImages/womanhand.svg",
 //                   BestBadgeUrl = "images/coursesImages/bestsellerbadge.svg",
 //                   BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                   Price = 11.20m,
 //                   Hours = 160,
 //                   RatingPercentage = 92,
 //                   RatingCount = "3.1K"
 //               },

 //                new() {
 //                    Title = "Creative CSS Drawing Course: Make Art With CSS",
 //                    Author = "By Robert Fox",
 //                    ImageUrl = "images/coursesImages/womannote.svg",
 //                    BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                    Price = 10.50m,
 //                    Hours = 220,
 //                    RatingPercentage = 94,
 //                    RatingCount = "4.2K"
 //                },

 //                 new() {
 //                     Title = "Blender Character Creator v2.0 for Video Games Design",
 //                     Author = "By Ralph Edwards",
 //                     ImageUrl = "images/coursesImages/womandesk.svg",
 //                     BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                     Price = 18.99m,
 //                     Hours = 160,
 //                     RatingPercentage = 92,
 //                     RatingCount = "3.1K"
 //                 },

 //                  new() {
 //                      Title = "The Ultimate Guide to 2D Mobile Game Development with Unity",
 //                      Author = "By Albert Flores",
 //                      ImageUrl = "images/coursesImages/manheadphones.svg",
 //                      BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                      RedPrice = 12.99m,
 //                      OldPrice = 44.99m,
 //                      Hours = 100,
 //                      RatingPercentage = 98,
 //                      RatingCount = "2.7K"
 //                  },

 //                   new() {
 //                       Title = "Learn JMETER from Scratch on Live Apps-Performance Testing",
 //                       Author = "By Jenny Wilson",
 //                       ImageUrl = "images/coursesImages/manlaptop.svg",
 //                       BookmarkUrl = "images/coursesImages/bookmark.svg",
 //                       Price = 14.50m,
 //                       Hours = 160,
 //                       RatingPercentage = 92,
 //                       RatingCount = "3.1K"
 //                   },
 //       };
}
