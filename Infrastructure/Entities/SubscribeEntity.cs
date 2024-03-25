using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscribeEntity
{
    [Key]
    public int Id { get; set; }

    public bool Newsletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;
    public bool Unsubscribed { get; set; } = false;
    public string Email { get; set; } = null!;
    public DateTime? Subscribed { get; set; }
    public ICollection<UserEntity>? Users { get; set; }
}
