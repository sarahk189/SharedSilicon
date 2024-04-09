using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController(DataContext context) : ControllerBase
{
    #region CREATE

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await context.Contacts.OrderBy(x => x.FullName).ToListAsync();

        return Ok(ContactFactory.Create(contacts));
    }
    #endregion
}
