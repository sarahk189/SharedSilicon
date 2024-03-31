﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Configurations;

public static class JwtConfiguration
{
    public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jtw:Secret"]!)),

                ClockSkew = TimeSpan.Zero,
            };
        });
                                                                          
    }
}
