using Microsoft.EntityFrameworkCore;

namespace PhotosiProducts.Model;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);
    }
}