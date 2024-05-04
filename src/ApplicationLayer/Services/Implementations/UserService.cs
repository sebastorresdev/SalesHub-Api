
using DomainLayer.Models;
using InfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Create(UserDto newUserDto)
    {
        try
        {
            var createUser = await _userRepository.Create(newUserDto.ToUserEntity());
            if (createUser.Id == 0) throw new TaskCanceledException("No se pudo crear el usuario");

            var userQuery = await _userRepository.GetAll(u => u.Id == createUser.Id);

            createUser = userQuery.Include(r => r.Rol).First();

            return createUser.ToUserDto();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var userFound = await _userRepository.GetById(id) ?? throw new TaskCanceledException("El usuario no existe");

            await _userRepository.Delete(userFound.Id);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Edit(UserDto userDto)
    {
        try
        {
            var user = userDto.ToUserEntity();

            var userFound = await _userRepository.GetById(user.Id) ?? throw new TaskCanceledException("El usuario no existe");

            userFound.FullName = user.FullName;
            userFound.Email = user.Email;
            userFound.Password = user.Password;
            userFound.RolId = user.RolId;
            userFound.IsActive = user.IsActive;

            await _userRepository.Update(userFound);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserDto>> GetAll()
    {
        try
        {
            var userQuery = await _userRepository.GetAll();
            var userList = userQuery.Include(r => r.Rol).ToList();
            return userList.Select(u => u.ToUserDto()).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SessionDto> ValidateCredentials(string email, string password)
    {
        try
        {
            var userQuery = await _userRepository.GetAll(u => u.Email == email && u.Password == password);
            if (userQuery.FirstOrDefault() is null) throw new TaskCanceledException("El usuario no existe");

            var user = userQuery.Include(r => r.Rol).First();

            return user.ToSessionDto();

        }
        catch (Exception)
        {
            throw;
        }
    }
}
