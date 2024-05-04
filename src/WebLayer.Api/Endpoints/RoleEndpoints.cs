using System.Text.RegularExpressions;
using ApplicationLayer;

namespace WebLayer.Api;

public static class RoleEndpoints
{
    public static RouteGroupBuilder MapRoleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Roles");

        group.MapGet("/", async (IRoleService _roleService) =>
        {
            try
            {
                var roles = await _roleService.GetAll();
                return Results.Ok(roles);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        return group;
    }
}
