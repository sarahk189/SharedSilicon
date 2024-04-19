using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factories;

public class ContactFactory
{

    public static ContactDto Create(ContactEntity entity)
    {
        try
        {

            return new ContactDto
            {
                
                FullName = entity.FullName,
                Email = entity.Email,
                Service = entity.Service,
                Message = entity.Message
            };

        }
        catch { }
        return null!;
    }

    public static IEnumerable<ContactDto> Create(List<ContactEntity> entities)
    {
        List<ContactDto> contacts = [];
        try
        {

            foreach (var entity in entities)
                contacts.Add(Create(entity));

        }
        catch { }
        return contacts!;
    }

}
