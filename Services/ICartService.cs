public interface ICartService
{
    Task<IEnumerable<Order>> GetCartItemsAsync(int userId);
    Task IncrementItemAsync(int userId, int productId);
    Task DecrementItemAsync(int userId, int productId);
    Task RemoveItemAsync(int userId, int productId);
    Task CheckoutAsync(int userId);
    Task<IEnumerable<Order>> GetOrderHistoryAsync(int userId);
    Task<IEnumerable<Order>> GetAllOrderHistoryAsync();
}
