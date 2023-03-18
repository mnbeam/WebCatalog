using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebCatalog.Api.Middlewares;
using WebCatalog.Api.UserAccessor;
using WebCatalog.Domain.Entities;
using WebCatalog.Infrastructure;
using WebCatalog.Infrastructure.DataBase;
using WebCatalog.Logic;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

var configuration = builder.Configuration;


builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserAccessor, UserAccessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddInfrastructure(configuration);
builder.Services.AddLogic(configuration);

var authOptions = configuration
    .GetSection(nameof(AuthOptions))
    .Get<AuthOptions>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authOptions.SymmetricSecurityKey
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var dbContext = scopedProvider.GetRequiredService<AppDbContext>();
        var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        await ApplicationDbContextSeed.SeedAsync(dbContext, userManager, roleManager, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();