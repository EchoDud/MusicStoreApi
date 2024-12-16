using Microsoft.EntityFrameworkCore;
using MusicStoreApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<VisitStat> VisitStats { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
        .HasOne<Category>()
        .WithMany()
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasDefaultValue("Client");

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasDefaultValue("Pending");


        modelBuilder.Entity<VisitStat>()
        .HasKey(vs => vs.Id);

        modelBuilder.Entity<VisitStat>()
            .Property(vs => vs.Month)
            .IsRequired();

        modelBuilder.Entity<VisitStat>()
            .Property(vs => vs.VisitsCount)
            .HasDefaultValue(0);
    }
}
