using GL_PROJ.Data;
using Microsoft.EntityFrameworkCore;
using GL_PROJ.Models.DBService;
using GL_PROJ.AppConfig;

// Buidler initialization
var builder = WebApplication.CreateBuilder(args);

// Adding all controllers with views
builder.Services.AddControllersWithViews();

// Adding a service to handle data by means of dependency injection
builder.Services.AddTransient<IDB, DB_Manager>();

// Configure session unit
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddSignalR();

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

// Using cookie policy
app.UseCookiePolicy();


// Enabling data transfer from "http://localhost:4200"
app.UseCors(options =>
    options
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Enabling project routing (page request - method link)
app.UseRouting();

app.MapDefaultControllerRoute();


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MainHub>("/hub");
});

// Application lanuch
app.Run();