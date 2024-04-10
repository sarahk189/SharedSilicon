
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Filters;
using Infrastructure.Dtos;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ContactController(DataContext context) : ControllerBase
{
	private readonly DataContext _context = context;

	[HttpPost("send")]
	[UseApiKey]
	public async Task<IActionResult> Send([FromBody] ContactDto input)
	{

		if (ModelState.IsValid)
		{
			if (string.IsNullOrEmpty(input.FullName) || string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Message))
			{
				return BadRequest("All fields are required.");
			}

			var contactRequest = new ContactRequestEntity
			{
				FullName = input.FullName,
				EmailAddress = input.Email,
				Message = input.Message
			};

			_context.ContactRequests.Add(contactRequest);

			await _context.SaveChangesAsync();
			return Ok();
		}
		return BadRequest(ModelState);

	}


}
