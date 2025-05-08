using CustomAth.Models;
using CustomAth.Services;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;

namespace CustomAth.Controllers;

public class CaisseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

    public CaisseController(AppDbContext context,IRazorViewToStringRenderer renderer)
    {
        _context = context;
        _razorViewToStringRenderer = renderer;

    }

    public IActionResult Index()
    {
        var categories = _context.Category.ToList(); // ✅ respecte le nom exact que TU as mis
        return View(categories); // hedhe l vue li mbaad shyet9asem en 3 partie 
    }
    
    public IActionResult GetCategories()
    {
        var categories = _context.Category.ToList();
        return PartialView("_Category", categories);
    }

    public IActionResult GetProductsByCategory(int categoryId)
    {
        var products = _context.Product
            .Where(p => p.CategoryId == categoryId)
            .ToList();

        return PartialView("_Product", products);

    }
    
    public IActionResult Annuler()
    {
        return PartialView("_Ticket","");
    }
    
    [HttpPost]
    public async Task<IActionResult> Encaisser([FromBody] List<TicketViewModel> produits)
    {
        // 1. Mettre à jour les quantités dans la base
        foreach (var p in produits)
        {
            var produit = _context.Product.FirstOrDefault(x => x.ProductName == p.Nom);
            if (produit != null)
            {
                produit.ProductQuantity -= p.Qte;
            }
        }

        // 2. Enregistrer le ticket
        var ticket = new Ticket
        {
            Date = DateTime.Now,
            Total = produits.Sum(p => p.PT),
            Lignes = produits.Select(p => new LigneTicket
            {
                NomProduit = p.Nom,
                Quantite = p.Qte,
                PrixUnitaire = p.PU,
                PrixTotal = p.PT
            }).ToList()
        };

        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        string html = await _razorViewToStringRenderer.RenderViewToStringAsync("Caisse/TicketPDF", ticket);
        var pdf = PdfGenerator.GeneratePdf(html, PaperKind.A4);

        return File(pdf, "application/pdf", "ticket.pdf");
    }

}