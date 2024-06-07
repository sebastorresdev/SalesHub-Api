using ApplicationLayer;
using InfrastructureLayer;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using WebLayer.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});

// CONFIGURACION DE LA CADENA DE CONEXION A LA BASE DE DATOS

var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddDbContext<SalesHubDbContext>(options =>
    options.UseSqlServer(connectionString)
);

/////////////////////////////////////////////////////////////////////////////////////////

// CONFIGURACION DE LOS REPOSITORIOS
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

/////////////////////////////////////////////////////////////////////////////////////////

// Configuración de los servicios
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IMenuService, MenuService>();


var app = builder.Build();

// Usa el middleware de CORS
app.UseCors("AllowSpecificOrigin");


app.MapDashboardEndpoints();
app.MapMenuEndpoints();
app.MapProductEndpoints();
app.MapCategoryEndpoints();
app.MapRoleEndpoints();
app.MapUserEndpoints();
app.MapSaleEndpoints();

app.Run();
