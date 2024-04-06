
namespace Infrastructure.Dtos;

public class ContactDto
{
	//do i need an id? no but i leave comment for future reference
	//public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public string EmailAddress { get; set; } = null!;
	public string Message { get; set; } = null!;	
}
