namespace DomainLayer.Models;

public partial class Category : EntityDB
{
    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
