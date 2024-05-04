using ApplicationLayer;

namespace WebLayer.Api;

public static class SaleEndpoints
{
    public static RouteGroupBuilder MapSaleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Venta");

        group.MapGet("/Historial", async (
            ISaleService _saleService,
            string search,
            string? saleNumber,
            string? startDate,
            string? endDate) =>
        {
            try
            {
                var response = await _saleService.Historical(
                    search,
                    saleNumber,
                    startDate,
                    endDate
                );
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapGet("/Reporte", async (
            ISaleService _saleService,
            string? startDate,
            string? endDate) =>
        {
            try
            {
                var response = await _saleService.Reports(
                    startDate,
                    endDate
                );
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });


        group.MapPost("/Registrar", async (SaleDto saleDto, ISaleService _saleService) =>
        {
            try
            {
                var response = await _saleService.Register(saleDto);
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
