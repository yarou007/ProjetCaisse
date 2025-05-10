using CustomAth.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomAth.Controllers;

public class VentesController : Controller
{
    private readonly AppDbContext _context;

    public VentesController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    public IActionResult Index()
    {
        return View(_context.Tickets.ToList());
    }
    
    public IActionResult Details(int id)
    {
        var ticket = _context.LigneTickets
            .Where(t => t.TicketId == id)
            .ToList();
        if (ticket == null)
        {
            return NotFound();
        }
        return View(ticket);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket is not null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Ventes");
    }

        


}