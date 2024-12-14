public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }

    // Навигационные свойства
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
