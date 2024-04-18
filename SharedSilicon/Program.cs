using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using SharedSilicon.Helpers.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SavedCoursesRepository>();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
b => b.MigrationsAssembly("Infrastructure")));

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.Cookie.SameSite = SameSiteMode.None;
	options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "371729882544755";
    x.AppSecret = "e50bb2b0018b70f57a6f03f050699f1a";
    x.Fields.Add("first_name");
	x.Fields.Add("last_name");
});

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error404", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
