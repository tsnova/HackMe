using HackMe.Infrastructure.Data;
using HackMe.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
        name: "notfound",
        pattern: "/Error/{action=PageNotFound}",
        defaults: new { controller = "Error" }
    );
app.MapControllerRoute(
        name: "error",
        pattern: "/Error/{action=Unhandled}",
        defaults: new { controller = "Error" }
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapRazorPages();

app.Run();
