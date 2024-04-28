using Microsoft.EntityFrameworkCore;
namespace test1.Models;

public class ApplicationContext:DbContext
{
    public DbSet<Tutorial> Tutorials { get; set; } = null;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}