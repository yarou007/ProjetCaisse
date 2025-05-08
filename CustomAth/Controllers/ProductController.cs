using CustomAth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomAth.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public IActionResult Index()
    {
        ViewBag.Categories = new SelectList(_context.Category, "Id", "CategoryName");

        return View(_context.Product.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Category, "Id", "CategoryName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductViewModel productViewModel)
    {

        var product = new Product()
        {
            ProductName = productViewModel.ProductName,
            ProductQuantity = productViewModel.ProductQuantity,
            ProductUnitPrice = productViewModel.ProductUnitPrice,
            CategoryId = productViewModel.CategoryId
        };
        await _context.Product.AddAsync(product);
        _context.SaveChanges();
        return RedirectToAction("Index", "Product");

    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Categories = new SelectList(_context.Category, "Id", "CategoryName");

        var product = await _context.Product.FindAsync(id);
        return View(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Product productViewModel)
    {
        var product = await _context.Product.FindAsync(productViewModel.Id);
        if (product is not null)
        {
            product.ProductName = productViewModel.ProductName;
            product.ProductUnitPrice = productViewModel.ProductUnitPrice;
            product.ProductQuantity = productViewModel.ProductQuantity;
            product.CategoryId= productViewModel.CategoryId;

            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Product");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product is not null)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Product");
    }
    
}