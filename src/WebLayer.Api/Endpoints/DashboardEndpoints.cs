using ApplicationLayer;

namespace WebLayer.Api;

public static class DashboardEndpoints
{
    public static RouteGroupBuilder MapDashboardEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Dashboard");

        group.MapGet("/Resumen", async (IDashboardService _dashboardService) =>
        {
            try
            {
                var response = await _dashboardService.Summary();
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
