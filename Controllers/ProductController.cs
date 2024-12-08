using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Brand = "Fender", Model = "Player Stratocaster", Color = "Sunburst", Description = "Идеальный выбор для начинающих и профессионалов.", Price = 120000, ImageUrl = "https://images.musicstore.de/images/1280/fender-player-plus-stratocaster-hss-mn-3-color-sunburst_1_GIT0056958-000.jpg", CategoryId = 3 },
        // Добавьте остальные продукты из моков
    };

    [HttpGet]
    public IActionResult GetProducts() => Ok(Products);

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public IActionResult GetProductsByCategory(int categoryId)
    {
        var products = Products.Where(p => p.CategoryId == categoryId).ToList();
        return Ok(products);
    }
}
