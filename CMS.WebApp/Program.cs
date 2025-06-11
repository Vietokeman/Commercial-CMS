using CMS.Core.ConfigOptions;
using CMS.Core.Domain.Identity;
using CMS.Data;
using CMS.WebApp.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//system config
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

//config environment for development and production
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllersWithViews();


//setup custom 
builder.Services.Configure<SystemConfig>(configuration.GetSection("SystemConfig"));

//set up DB for web app
builder.Services.AddDbContext<CMSDbContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, AppRole>(o => o.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CMSDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
