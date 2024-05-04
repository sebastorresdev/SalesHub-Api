namespace ApplicationLayer;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
}
