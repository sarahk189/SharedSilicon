using Infrastructure.Contexts;
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
    public IActionResult GetToken(SignIn form)
    {
        if (ModelState.IsValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new(ClaimTypes.Email, form.Email)
                //}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);

        }
        return Unauthorized();
    }
}
