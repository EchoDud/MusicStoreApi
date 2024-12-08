using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] 
public class CategoryController : ControllerBase
{
    private static readonly List<Category> Categories = new()
    {
        new Category { Id = 1, Name = "Струнные", ParentId = null },
        new Category { Id = 2, Name = "Гитары", ParentId = 1 },
        // Добавьте остальные категории
    };

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(Categories);
    }

    [HttpGet("{id}/children")]
    public IActionResult GetChildCategories(int id)
    {
        var children = Categories.Where(c => c.ParentId == id).ToList();
        return Ok(children);
    }
}
