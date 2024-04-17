using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Filters;
using WebApi.Models;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(DataContext dataContext, IConfiguration configuration) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _configuration = configuration;

	[UseApiKey]
	[HttpPost]
	[Route("token")]
	public IActionResult GetToken([FromBody] SimpleClaimDto[] claims)
	{
		try
		{
			var claimObjects = claims.Select(c => new Claim(c.Type, c.Value)).ToArray();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claimObjects),
				Expires = DateTime.UtcNow.AddDays(1),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(tokenString);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

}
