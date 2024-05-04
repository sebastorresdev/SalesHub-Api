
using DomainLayer.Models;
using InfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer;

public class MenuService : IMenuService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<MenuRole> _menuRoleRepository;
    private readonly IGenericRepository<Menu> _menuRepository;

    public MenuService(IGenericRepository<User> userRepository, IGenericRepository<MenuRole> menuRoleRepository, IGenericRepository<Menu> menuRepository)
    {
        _userRepository = userRepository;
        _menuRoleRepository = menuRoleRepository;
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuDto>> GetAll(int id)
    {
        var user = await _userRepository.GetById(id);
        var menuRole = await _menuRoleRepository.GetAll();
        var menu = await _menuRepository.GetAll();

        try
        {
            if (user is null) throw new Exception("No se obtuvieron las listas");

            var result = menuRole.Where(mr => mr.RolId == user.RolId)
                                 .Select(mr => mr.Menu!.ToMenuDto())
                                 .ToList();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
