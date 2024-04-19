namespace Infrastructure.Dtos;

public class UserDto
{
	public Guid UserId { get; set; }
	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string? Phone { get; set; }
	public string? Biography { get; set; }
	public string? ProfileImage { get; set; }
}
