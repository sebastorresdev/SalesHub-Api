namespace DomainLayer.Models;

public partial class MenuRole : EntityDB
{
    public int? RolId { get; set; }

    public int? MenuId { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual Role? Rol { get; set; }
}
