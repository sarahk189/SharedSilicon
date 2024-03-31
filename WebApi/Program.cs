using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.RegisterJwt(builder.Configuration);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();


app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle