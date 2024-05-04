namespace DomainLayer.Models;

public partial class Role : EntityDB
{
    public string? Name { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
