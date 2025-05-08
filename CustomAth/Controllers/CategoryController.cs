using CustomAth.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomAth.Controllers;

public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public IActionResult Index()
    {
        return View(_context.Category.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryViewModel categoryViewModel)
    {

        var category = new Category
        {
            CategoryName = categoryViewModel.CategoryName
        };
        await _context.Category.AddAsync(category);
        _context.SaveChanges();
        return RedirectToAction("Index", "Category");

    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _context.Category.FindAsync(id);
        return View(category);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Category categoryViewModel)
    {
        var category = await _context.Category.FindAsync(categoryViewModel.Id);
        if (category is not null)
        {
            category.CategoryName = categoryViewModel.CategoryName;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Category");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var catagory = await _context.Category.FindAsync(id);
        if (catagory is not null)
        {
            _context.Category.Remove(catagory);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Category");
    }
    
}