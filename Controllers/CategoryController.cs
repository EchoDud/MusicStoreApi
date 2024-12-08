using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] 
public class CategoryController : ControllerBase
{
    private static readonly List<Category> Categories = new()
    {
        new Category { Id = 1, Name = "��������", ParentId = null },
        new Category { Id = 2, Name = "������", ParentId = 1 },
        // �������� ��������� ���������
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
