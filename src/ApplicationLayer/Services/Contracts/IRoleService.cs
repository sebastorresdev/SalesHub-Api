namespace ApplicationLayer;

public interface IRoleService
{
    Task<List<RoleDto>> GetAll();
}
