using System;
using System.Threading.Tasks;

public class TestDataSeeder
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public TestDataSeeder(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task SeedDataAsync()
    {
        // ���������, ��� ���� ������
        var categories = await _categoryService.GetCategoriesAsync();
        var products = await _productService.GetProductsAsync();

        if (categories.Any() || products.Any())
        {
            Console.WriteLine("���� ������ ��� �������� ������. ������� �� ������� ����� �����������.");
            return;
        }

        // ������ ���������
        var stringsCategory = new Category { Name = "��������", ParentId = null };
        await _categoryService.AddCategoryAsync(stringsCategory);

        var guitarsCategory = new Category { Name = "������", ParentId = stringsCategory.Id };
        await _categoryService.AddCategoryAsync(guitarsCategory);

        var drumsCategory = new Category { Name = "�������", ParentId = null };
        await _categoryService.AddCategoryAsync(drumsCategory);

        Console.WriteLine("��������� �������.");

        // ������ ��������
        var product1 = new Product
        {
            Brand = "Fender",
            Model = "Stratocaster",
            Color = "Sunburst",
            Description = "����������� �������������.",
            Price = 120000,
            ImageUrl = "https://example.com/images/stratocaster.jpg",
            CategoryId = guitarsCategory.Id
        };
        await _productService.AddProductAsync(product1);

        var product2 = new Product
        {
            Brand = "Tama",
            Model = "Imperialstar",
            Color = "Black",
            Description = "������ �������� �������.",
            Price = 85000,
            ImageUrl = "https://example.com/images/imperialstar.jpg",
            CategoryId = drumsCategory.Id
        };
        await _productService.AddProductAsync(product2);

        Console.WriteLine("�������� �������.");
    }
}
