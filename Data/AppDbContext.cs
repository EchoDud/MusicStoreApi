using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ����� ����� Product � Category
        modelBuilder.Entity<Product>()
            .HasOne<Category>() // ���� ������� ������ � ����� ����������
            .WithMany()         // ���� ��������� ����� ��������� ����� ���������
            .HasForeignKey(p => p.CategoryId) // ������� ����
            .OnDelete(DeleteBehavior.Cascade); // ��� �������� ��������� ��������� ��������� ��������
    }
}
