namespace CustomAth.Models;

public class Ticket
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }

    // Liste de tous les articles achetés dans le ticket
    public List<LigneTicket> Lignes { get; set; }
}
