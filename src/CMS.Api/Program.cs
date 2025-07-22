using CMS.Api;
using CMS.Api.Authorization;
using CMS.Api.Filters;
using CMS.Api.Service;
using CMS.Core.ConfigOptions;
using CMS.Core.Domain.Identity;
using CMS.Core.Factories;
using CMS.Core.Models.Content;
using CMS.Core.Repositories;
using CMS.Core.SeedWorks;
using CMS.Core.Services;
using CMS.Core.Strategies;
using CMS.Data;
using CMS.Data.Repositories;
using CMS.Data.SeedWorks;
using CMS.Data.Service;
using CMS.Data.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using DataFactory = CMS.Data.SqlServer.SqlServerRepositoryFactory;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var VietokemanPolicy = "VietokemanPolicy";

    builder.Host.UseSerilog();

    builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
    builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    builder.Services.AddScoped<IRepositoryFactory, DataFactory>();
    builder.Services.AddCors(o => o.AddPolicy(VietokemanPolicy, builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(configuration["AllowedOrigins"])
        .AllowCredentials();
    }));

    builder.Services.AddDbContext<CMSDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<CMSDbContext>()
    .AddDefaultTokenProviders();
    //if (config["Repository:Provider"] == "SqlServer")
    //{
    //    services.AddScoped<IRepositoryFactory, SqlServerRepositoryFactory>();
    //}
    //else if (config["Repository:Provider"] == "Postgre")
    //{
    //    services.AddScoped<IRepositoryFactory, PostgreRepositoryFactory>();
    //}
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));

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

    builder.Services.AddAutoMapper(typeof(PostInListDto));

    builder.Services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
    builder.Services.Configure<MediaSettings>(configuration.GetSection("MediaSettings"));
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IRoyaltyService, RoyaltyService>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.CustomOperationIds(apiDesc =>
        {
            return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });
        c.SwaggerDoc("AdminAPI", new OpenApiInfo
        {
            Version = "v1",
            Title = "API for Administrators",
            Description = "API for CMS core domain. This domain keeps track of campaigns, campaign rules, and campaign execution."
        });
        c.ParameterFilter<SwaggerNullableParameterFilter>();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: Bearer {token}",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                new string[] {}
            }
        });
    });

    builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = configuration["JwtTokenSettings:Issuer"],
            ValidAudience = configuration["JwtTokenSettings:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenSettings:Key"]))
        };
    });
    builder.Services.AddScoped<IPostSearchStrategy, SearchBySlugStrategy>();
    builder.Services.AddScoped<SearchBySlugStrategy>();
    builder.Services.AddScoped<SearchByCategoryStrategy>();
    builder.Services.AddScoped<SearchContext>();



    var app = builder.Build();

    app.UseStaticFiles();
    app.UseSerilogRequestLogging(); // Log HTTP request pipeline

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/AdminAPI/swagger.json", "Admin API");
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.InjectStylesheet("/swagger-custom.css");
        });
    }

    app.UseCors(VietokemanPolicy);

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.MigrateDatabase();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
