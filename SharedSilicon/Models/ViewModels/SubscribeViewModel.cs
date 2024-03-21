using SharedSilicon.Models;

namespace SharedSilicon.ViewModels;

public class SubscribeViewModel
{
    public string Title { get; set; } = "Home";
    public SubscribeModel Form { get; set; } = new SubscribeModel();

    public bool Newletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;

}
