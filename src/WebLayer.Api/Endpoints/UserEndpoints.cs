using ApplicationLayer;

namespace WebLayer.Api;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Usuario");

        group.MapGet("/", async (IUserService _userService) =>
        {
            try
            {
                var response = await _userService.GetAll();
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("/IniciarSesion", async (LoginDto loginDto, IUserService _userService) =>
        {
            try
            {
                var response = await _userService.ValidateCredentials(loginDto.Email, loginDto.Password);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPost("/Guardar", async (UserDto userDto, IUserService _userService) =>
        {
            try
            {
                var response = await _userService.Create(userDto);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPut("/Editar", async (UserDto userDto, IUserService _userService) =>
        {
            try
            {
                await _userService.Edit(userDto);
                return Results.Ok(userDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapDelete("/Eliminar/{id:int}", async (int id, IUserService _userService) =>
        {
            try
            {
                await _userService.Delete(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        return group;
    }
}
