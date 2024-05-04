using ApplicationLayer;

namespace WebLayer.Api;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Productos");

        group.MapGet("/", async (IProductService _productService) =>
        {
            try
            {
                var response = await _productService.GetAll();
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Ok(ex.Message);
            }
        });

        group.MapPost("/Guardar", async (ProductDto productDto, IProductService _productService) =>
        {
            try
            {
                var response = await _productService.Create(productDto);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapPut("/Editar", async (ProductDto productDto, IProductService _productService) =>
        {
            try
            {
                await _productService.Edit(productDto);
                return Results.Ok(productDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        group.MapDelete("/Eliminar/{id:int}", async (int id, IProductService _productService) =>
        {
            try
            {
                await _productService.Delete(id);
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
