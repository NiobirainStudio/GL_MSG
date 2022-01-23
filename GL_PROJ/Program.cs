using GL_PROJ.Data;
using Microsoft.EntityFrameworkCore;

// Buidler initialization
var builder = WebApplication.CreateBuilder(args);

// Adding all controllers with views
builder.Services.AddControllersWithViews();

// Connecting a database
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration["conStr"])
);

// Building the application
var app = builder.Build();


// Setting the Error method from the Home controller in case of non development environment
if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

// Using static files a. k. a. wwwroot files?
app.UseStaticFiles();

// Enabling project routing (page request - method link)
app.UseRouting();

//app.UseAuthorization();

// Using the Home controller method Login (GET)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

// Application lanuch
app.Run();