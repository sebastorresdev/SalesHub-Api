using ApplicationLayer;

namespace WebLayer.Api;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Categorias");

        group.MapGet("/", async (ICategoryService _categoryService) =>
        {
            try
            {
                var response = await _categoryService.GetAll();
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
