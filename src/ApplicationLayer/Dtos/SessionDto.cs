namespace ApplicationLayer;

public record SessionDto(
    int Id,
    string? FullName,
    string? Email,
    string? RoleDescription
);