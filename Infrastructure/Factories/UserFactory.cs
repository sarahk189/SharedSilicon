using Infrastructure.Entities;


namespace Infrastructure.Factories;

public static class UserFactory
{

	public static UserEntity CreateUser(string firstName, string lastName, string email)
	{
		return new UserEntity
		{
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			UserName = email
		};
	}
}
