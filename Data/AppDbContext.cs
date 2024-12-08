using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связь между Product и Category
        modelBuilder.Entity<Product>()
            .HasOne<Category>() // Один продукт связан с одной категорией
            .WithMany()         // Одна категория может содержать много продуктов
            .HasForeignKey(p => p.CategoryId) // Внешний ключ
            .OnDelete(DeleteBehavior.Cascade); // При удалении категории удаляются связанные продукты
    }
}
