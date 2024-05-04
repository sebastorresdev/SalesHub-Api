namespace DomainLayer.Models;

public partial class Menu : EntityDB
{
    public string? Name { get; set; }

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}
