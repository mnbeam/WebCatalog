using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Abstractions;

namespace WebCatalog.Infrastructure.DataBase;

public class ApplicationDbContext : AppDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}