namespace DomainLayer.Models;

public partial class Product : EntityDB
{
    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
