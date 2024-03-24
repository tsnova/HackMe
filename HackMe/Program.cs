using HackMe.Application.AutoMapperProfiles;
using HackMe.Application.Services;
using HackMe.Infrastructure.Data;
using HackMe.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication("customAuthentication")
    .AddCookie("customAuthentication", options =>
    {
        options.LoginPath = "/Home";
    });

builder.Services.AddAutoMapper(typeof(AgentProfile));

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IAgentService, AgentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error/Unhandled");
}
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//        name: "profile",
//        pattern: "/Profile/{action}/{codeName}",
//        defaults: new { controller = "Profile", action = "Index" }
//    );

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
        pattern: "/{controller}/{action}/{id?}",
        defaults: new { controller = "Home", action = "Index" }

    );

app.MapRazorPages();

app.Run();
