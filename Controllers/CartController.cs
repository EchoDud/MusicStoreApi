using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItems()
    {
        var userId = GetUserId();
        var items = await _cartService.GetCartItemsAsync(userId);

        // Возвращаем данные с полями, связанными с продуктом
        return Ok(items.Select(item => new
        {
            item.Id,
            item.ProductId,
            item.Product.Brand,
            item.Product.Model,
            item.Product.Price,
            item.Count
        }));
    }

    [HttpPost("{productId}/increment")]
    public async Task<IActionResult> IncrementItem(int productId)
    {
        var userId = GetUserId();
        await _cartService.IncrementItemAsync(userId, productId);
        return Ok();
    }

    [HttpPost("{productId}/decrement")]
    public async Task<IActionResult> DecrementItem(int productId)
    {
        var userId = GetUserId();
        await _cartService.DecrementItemAsync(userId, productId);
        return Ok();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveItem(int productId)
    {
        var userId = GetUserId();
        await _cartService.RemoveItemAsync(userId, productId);
        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var userId = GetUserId();
        await _cartService.CheckoutAsync(userId);
        return Ok();
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetOrderHistory()
    {
        var userId = GetUserId(); // Получение userId из токена
        var orders = await _cartService.GetOrderHistoryAsync(userId);

        return Ok(orders.Select(order => new
        {
            order.Id,
            order.ProductId,
            order.Product.Brand,
            order.Product.Model,
            order.Price,
            order.Count,
            order.Status,
            order.CreatedDate
        }));
    }

    [HttpGet("all-history")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrderHistory()
    {
        var orders = await _cartService.GetAllOrderHistoryAsync();
        return Ok(orders);
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }
        return int.Parse(userIdClaim.Value);
    }
}
