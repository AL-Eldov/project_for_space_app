using Microsoft.EntityFrameworkCore;
using project_for_space_app.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddControllersWithViews();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=ShowUsers}/{id?}");

app.Run();