namespace CustomAth.Models;

public class LigneTicket
{
    public int Id { get; set; }
    public string NomProduit { get; set; }
    public int Quantite { get; set; }
    public decimal PrixUnitaire { get; set; }
    public decimal PrixTotal { get; set; }

    // FK vers le ticket parent
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}
