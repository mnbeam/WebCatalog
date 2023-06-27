using WebCatalog.Api.Extensions;
using WebCatalog.Api.Middlewares;
using WebCatalog.Api.UserAccessor;
using WebCatalog.Infrastructure;
using WebCatalog.Logic;
using WebCatalog.Logic.Common.Configurations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserAccessor, UserAccessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddInfrastructure(configuration, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files"));
builder.Services.AddLogic(configuration);

builder.Services.AddAuth(configuration);

var app = builder.Build();

await app.Services.SeedDatabaseAsync(app.Logger);

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