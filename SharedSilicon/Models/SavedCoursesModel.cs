using System.ComponentModel.DataAnnotations;


namespace SharedSilicon.Models;

public class SavedCoursesModel   
{
    [DataType(DataType.ImageUrl)]
    public string? ProfileImage { get; set; }
     
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = null!;
    public class MyCourse
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? BestBadgeUrl { get; set; }
        public string BookmarkUrl { get; set; } = null!;
        public decimal? Price { get; set; }
        public decimal? RedPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public int Hours { get; set; }
        public int RatingPercentage { get; set; }
        public string RatingCount { get; set; } = null!;
    }

    public List<MyCourse> MyCourses { get; set; } = [];
}
