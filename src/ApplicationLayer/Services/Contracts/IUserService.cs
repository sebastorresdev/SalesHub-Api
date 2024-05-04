namespace ApplicationLayer;

public interface IUserService
{
    Task<List<UserDto>> GetAll();
    Task<SessionDto> ValidateCredentials(string email, string password);
    Task<UserDto> Create(UserDto newUserDto);
    Task Edit(UserDto userDto);
    Task Delete(int Id);
}
