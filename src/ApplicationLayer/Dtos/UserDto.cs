namespace ApplicationLayer;

public record UserDto(
    int Id,
    string? FullName,
    int? RolId,
    string? Email,
    string? Password,
    int? IsActive,
    string? RoleDescription
);
