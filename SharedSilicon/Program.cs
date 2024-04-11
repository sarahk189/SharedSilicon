using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddHttpClient();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
b => b.MigrationsAssembly("Infrastructure")));
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
});

builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "371729882544755";
    x.AppSecret = "e50bb2b0018b70f57a6f03f050699f1a";
    x.Fields.Add("first_name");
	x.Fields.Add("last_name");
});

var app = builder.Build();


app.UseHsts();
app.UseStatusCodePagesWithReExecute("/Error404", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
//app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
