using DomainLayer.Models;
using InfrastructureLayer;


namespace ApplicationLayer;

public class RoleService : IRoleService
{
    private readonly IGenericRepository<Role> _roleRepository;

    public RoleService(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleDto>> GetAll()
    {
        try
        {
            var roleList = await _roleRepository.GetAll();
            return [.. roleList.Select(r => r.ToRoleDto())];
        }
        catch (Exception)
        {
            throw;
        }
    }
}
