namespace DomainLayer.Models;

public partial class DocumentNumber : EntityDB
{
    public int LastNumber { get; set; }

    public DateTime? CreationDate { get; set; }
}
