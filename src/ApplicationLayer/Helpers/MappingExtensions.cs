using System.Globalization;
using DomainLayer.Models;

namespace ApplicationLayer;

public static class MappingExtensions
{
    #region Role
    public static RoleDto ToRoleDto(this Role role) =>
        new(
            role.Id,
            role.Name
        );


    public static Role ToRoleEntity(this RoleDto roleDto) =>
        new() { Name = roleDto.Name };

    #endregion

    #region Menu
    public static MenuDto ToMenuDto(this Menu menu) =>
        new(
            menu.Id,
            menu.Name,
            menu.Icon,
            menu.Url
        );

    public static Menu ToMenuEntity(this MenuDto menuDto) =>
        new()
        {
            Id = menuDto.Id,
            Name = menuDto.Name,
            Icon = menuDto.Icon,
            Url = menuDto.Url
        };

    #endregion

    #region User

    public static UserDto ToUserDto(this User user) =>
        new(
            user.Id,
            user.FullName,
            user.RolId,
            user.Email,
            user.Password,
            (user.IsActive ?? false) ? 1 : 0,
            user.Rol!.Name
        );

    public static User ToUserEntity(this UserDto userDto) =>
        new()
        {
            Id = userDto.Id,
            FullName = userDto.FullName,
            RolId = userDto.RolId,
            Email = userDto.Email,
            Password = userDto.Password,
            IsActive = userDto.IsActive == 1,
        };

    public static SessionDto ToSessionDto(this User user) =>
        new(
            user.Id,
            user.FullName,
            user.Email,
            user.Rol!.Name
        );

    #endregion

    #region Category

    public static CategoryDto ToCategoryDto(this Category category) =>
        new(category.Id, category.Name);

    public static Category ToCategoryEntity(this CategoryDto categoryDto) =>
        new()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };

    #endregion

    #region Products

    public static ProductDto ToProductDto(this Product product) =>
        new(
            product.Id,
            product.Name,
            product.CategoryId,
            product.Category?.Name,
            product.Stock,
            Convert.ToString(product.Price, new CultureInfo("es-PE")),
            (product.IsActive ?? false) ? 1 : 0
        );

    public static Product ToProductEntity(this ProductDto productDto) =>
        new()
        {
            Id = productDto.Id,
            Name = productDto.Name,
            CategoryId = productDto.CategoryId,
            Stock = productDto.Stock,
            Price = Convert.ToDecimal(productDto.Price, new CultureInfo("es-PE")),
            IsActive = productDto.IsActive == 1
        };

    #endregion

    #region Sale

    public static SaleDto ToSaleDto(this Sale sale) =>
        new(
            sale.Id,
            sale.DocumentNumber,
            sale.PaymentMethod,
            Convert.ToString(sale.TotalPrice, new CultureInfo("es-PE")),
            sale.CreationDate?.ToString("dd/MM/yyyy"),
            sale.SalesDetails.Select(sd => sd.ToSalesDetailDto()).ToList()
        );

    public static Sale ToSaleEntity(this SaleDto saleDto) =>
        new()
        {
            Id = saleDto.Id,
            DocumentNumber = saleDto.DocumentNumber,
            PaymentMethod = saleDto.PaymentMethod,
            TotalPrice = Convert.ToDecimal(saleDto.TotalPrice, new CultureInfo("es-PE")),
            CreationDate = Convert.ToDateTime(saleDto.CreationDate)
        };
    #endregion

    #region SaleDetail

    public static SalesDetailDto ToSalesDetailDto(this SalesDetail salesDetail) =>
        new(
            salesDetail.ProductId,
            salesDetail.Product?.Name,
            salesDetail.Quantity,
            Convert.ToString(salesDetail.Price, new CultureInfo("es-PE")),
            Convert.ToString(salesDetail.TotalPrice, new CultureInfo("es-PE"))
        );

    public static SalesDetail ToSalesDetailEntity(this SalesDetailDto salesDetailDto) =>
        new()
        {
            ProductId = salesDetailDto.ProductoId,
            Quantity = salesDetailDto.Quantity,
            Price = Convert.ToDecimal(salesDetailDto.Price, new CultureInfo("es-PE")),
            TotalPrice = Convert.ToDecimal(salesDetailDto.TotalPrice, new CultureInfo("es-PE")),
        };

    #endregion

    #region Report

    public static ReportDto ToReportDto(this SalesDetail salesDetail) =>
        new(
            salesDetail.Sale?.DocumentNumber,
            salesDetail.Sale?.PaymentMethod,
            salesDetail.Sale?.CreationDate?.ToString("dd/MM/yyyy"),
            Convert.ToString(salesDetail.Sale?.TotalPrice, new CultureInfo("es-PE")),
            salesDetail.Product?.Name,
            salesDetail.Quantity,
            Convert.ToString(salesDetail.Price, new CultureInfo("es-PE")),
            Convert.ToString(salesDetail.TotalPrice, new CultureInfo("es-PE"))
        );

    #endregion

}
