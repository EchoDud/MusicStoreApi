using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetCartItemsAsync(int userId)
    {
        return await _context.Orders
            .Include(order => order.Product) // Загружаем связанные продукты
            .Where(order => order.UserId == userId && order.Status == "Pending") // Только те, которые в корзине
            .ToListAsync();
    }

    public async Task IncrementItemAsync(int userId, int productId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId && o.Status == "Pending");

        if (order == null)
        {
            var product = await _context.Products.FindAsync(productId)
                ?? throw new InvalidOperationException("Product not found.");

            order = new Order
            {
                UserId = userId,
                ProductId = productId,
                Price = product.Price,
                Count = 1
            };
            _context.Orders.Add(order);
        }
        else
        {
            order.Count++;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DecrementItemAsync(int userId, int productId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId && o.Status == "Pending");

        if (order == null) throw new InvalidOperationException("Item not found in cart.");

        if (order.Count > 1)
        {
            order.Count--;
        }
        else
        {
            _context.Orders.Remove(order);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int userId, int productId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId && o.Status == "Pending");

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetOrderHistoryAsync(int userId)
    {
        return await _context.Orders
            .Include(order => order.Product) // Подключаем продукт
            .Where(order => order.UserId == userId &&
                            (order.Status == "Paid" || order.Status == "Received" || order.Status == "Cancelled"))
            .ToListAsync();
    }

    public async Task CheckoutAsync(int userId)
    {
        var orders = await _context.Orders
            .Where(o => o.UserId == userId && o.Status == "Pending")
            .ToListAsync();

        if (!orders.Any()) throw new InvalidOperationException("Cart is empty.");

        foreach (var order in orders)
        {
            order.Status = "Paid"; // Меняем статус на оплачено
            order.CreatedDate = DateTime.Now; // Фиксируем дату
        }

        await _context.SaveChangesAsync();
    }
}
