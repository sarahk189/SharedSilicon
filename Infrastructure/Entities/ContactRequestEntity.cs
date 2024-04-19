namespace Infrastructure.Entities;

public class ContactRequestEntity
{
	public int Id { get; set; }
	public  string FullName { get; set; } = null!;
	public string EmailAddress { get; set; } = null!;
	public string Message { get; set; } = null!;
}
