public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Навигационные свойства
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;
}
