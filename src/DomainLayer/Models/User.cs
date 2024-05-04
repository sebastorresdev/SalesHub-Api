namespace DomainLayer.Models;

public partial class User : EntityDB
{
    public string? FullName { get; set; }

    public int? RolId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Role? Rol { get; set; }
}
