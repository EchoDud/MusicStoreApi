using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class TestDataSeeder
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public TestDataSeeder(ICategoryService categoryService, IProductService productService, IUserService userService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _userService = userService;
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
            ImageUrl = "https://images.musicstore.de/images/1600/fender-player-ii-stratocaster-rw-3-color-sunburst_1_GIT0061901-001.jpg",
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
            ImageUrl = "https://tama.ru/upload/resize_cache/iblock/82b/trlpm40afdq21i2ozp4csikfwsrjzrk7/800_0_1/A155679_318839_8.jpg",
            CategoryId = drumsCategory.Id
        };
        await _productService.AddProductAsync(product2);

        Console.WriteLine("�������� �������.");

        var admin1 = new User
        {
            Email = "admin@mail.com",
            PasswordHash = "password",
            Role = "Admin"
        };

        await  _userService.RegisterAsync(admin1.Email,admin1.PasswordHash,admin1.Role);

    }
}
