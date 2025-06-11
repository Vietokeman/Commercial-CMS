using CMS.Core.ConfigOptions;
using CMS.Core.Domain.Identity;
using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;
using CMS.Data;
using CMS.Data.Repositories;
using CMS.Data.SeedWorks;
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

// Add service to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));

// Auto Mapper service
builder.Services.AddAutoMapper(typeof(PostInListDto));

// Authentication and Authorization
//no tu co usermanager cac thu roi
// Auto DI for business services and repositories
var services = typeof(PostRepository).Assembly.GetTypes()
    .Where(x => x.GetInterfaces()
        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))
        && !x.IsAbstract && x.IsClass && x != typeof(RepositoryBase<,>));

foreach (var service in services)
{
    var allInterfaces = service.GetInterfaces();
    var directInterface = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces())).FirstOrDefault();
    if (directInterface != null)
    {
        builder.Services.Add(new ServiceDescriptor(directInterface, service, ServiceLifetime.Scoped));
    }
}

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
