namespace ApplicationLayer;

public interface IMenuService
{
    Task<List<MenuDto>> GetAll(int id);
}
