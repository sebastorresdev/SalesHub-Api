using ApplicationLayer;

namespace WebLayer.Api;

public static class MenuEndpoints
{
    public static RouteGroupBuilder MapMenuEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Menu/{id:int}");

        group.MapGet("/", async (IMenuService __menuService, int id) =>
        {
            try
            {
                var response = await __menuService.GetAll(id);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        return group;
    }
}
