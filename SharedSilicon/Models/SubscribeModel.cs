using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedSilicon.Models;

public class SubscribeModel
{
    [Display(Name = "Daily Newletter", Order = 0)]
    public bool Newsletter { get; set; } = false;

    [Display(Name = "Advertising Updates", Order = 1)]
    public bool AdvertisingUpdates { get; set; } = false;

    [Display(Name = "Week in Review", Order = 2)]
    public bool WeekInReview { get; set; } = false;

    [Display(Name = "Event Updates", Order = 3)]
    public bool EventUpdates { get; set; } = false;

    [Display(Name = "Startups Weekly", Order = 4)]
    public bool StartupsWeekly { get; set; } = false;

    [Display(Name = "Podcasts", Order = 5)]
    public bool Podcasts { get; set; } = false;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 6)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Email must be in the format of x@x.xx")]
    public string Email { get; set; } = null!;
}
