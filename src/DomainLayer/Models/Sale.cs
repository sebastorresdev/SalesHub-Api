namespace DomainLayer.Models;

public partial class Sale : EntityDB
{
    public string? DocumentNumber { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
