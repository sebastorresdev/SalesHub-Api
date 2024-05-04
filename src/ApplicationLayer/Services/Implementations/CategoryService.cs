using DomainLayer.Models;
using InfrastructureLayer;

namespace ApplicationLayer;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        try
        {
            var categories = await _categoryRepository.GetAll();
            return categories.Select(c => c.ToCategoryDto()).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
