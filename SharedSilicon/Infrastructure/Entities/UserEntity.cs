using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

//public class UserEntity : IdentityUser
//{
//	[ProtectedPersonalData]
//	public string FirstName { get; set; } = null!;

//	[ProtectedPersonalData]
//	public string LastName { get; set; } = null!;

//	public int? AddressId { get; set; }
//	public AddressEntity? Address { get; set; }
//}

public class UserEntity
{
	[Key]
	public int Id { get; set; } 
	public string FirstName { get; set; } = null!;	

	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string SecurityKey { get; set; } = null!;
	public string? Phone { get; set; }
	public string? Biography { get; set; }
	public string? ProfileImage { get; set; }

	public DateTime? Created { get; set; }
	public DateTime? Modified { get; set; }

	public int? AddressId { get; set; }
	public AddressEntity? Address { get; set; }
}
