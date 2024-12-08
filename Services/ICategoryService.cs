public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentId);
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}
