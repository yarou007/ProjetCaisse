using CustomAth.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomAth.Controllers;

public class DashboardController : Controller
{   
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var today = DateTime.Today;

        ViewBag.SalesToday = _context.Tickets
            .Where(t => t.Date.Date == today)
            .Sum(t => (double?)t.Total) ?? 0;

        ViewBag.TicketsCount = _context.Tickets.Count();

        ViewBag.TotalProducts = _context.Product.Count();

        var lowStockProducts = _context.Product
            .Where(p => p.ProductQuantity <= 10)
            .ToList();
        
        var mostSaledProduct = _context.LigneTickets
            .GroupBy(l => l.NomProduit)
            .Select(g => new
            {
                NomProduit = g.Key
                , TotalQuantity = g.Sum(l => l.Quantite)
            })
            .OrderByDescending(l => l.TotalQuantity)
            .Take(3)
            .ToList();
        
        
        ViewBag.mostSoldProductCount = mostSaledProduct.Count;
        ViewBag.mostSoldProduct = mostSaledProduct;

        ViewBag.LowStockCount = lowStockProducts.Count;
        ViewBag.LowStockProducts = lowStockProducts;

        return View();

    }
}