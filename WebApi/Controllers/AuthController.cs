using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;


//////namespace WebApi.Controllers;

//[Route("api/[controller]")]
//[ApiController]
public class AuthController(DataContext dataContext, IConfiguration configuration) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _configuration = configuration;

    //    //[UseApiKey]
    //    [HttpPost]
    //    [Route("token")]
    //    public IActionResult GetToken(/*SignIn form*/)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var key = Encoding.UTF8.GetBytes(_configuration["Jwl:Secret"]!);
    //            var tokenDescriptor = new SecurityTokenDescriptor
    //            {
    //                Subject = new ClaimsIdentity(new Claim[]
    //                {
    //                    //new(ClaimTypes.Email, form.Email),
    //                    //new(ClaimTypes.Name, form.Email)
    //                }),
    //                Expires = DateTime.UtcNow.AddDays(1),
    //                Issuer = _configuration["Jwt:Issuer"],
    //                Audience = _configuration["Jwt:Audience"],
    //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //            };

    //            var token = tokenHandler.CreateToken(tokenDescriptor);
    //            var tokenString = tokenHandler.WriteToken(token);

    //            return Ok(tokenString);

    //        }
    //        return Unauthorized();
    //    }
}
