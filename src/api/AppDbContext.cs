using Microsoft.EntityFrameworkCore;

using common;

namespace api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public required DbSet<Product> Products { get; set; }
}
