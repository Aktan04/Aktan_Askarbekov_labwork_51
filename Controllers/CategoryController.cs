using Microsoft.AspNetCore.Mvc;
using ProductShop.Models;

namespace ProductShop.Controllers;

public class CategoryController : Controller
{
    private ProductContext _context;

    public CategoryController(ProductContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (string.IsNullOrEmpty(category.NameOfCategory))
        {
            ModelState.AddModelError("Name", "Заполните название бренда!");
        }
        else if (_context.Categories.Any(b => b.NameOfCategory.ToLower().Trim() == category.NameOfCategory.ToLower().Trim()))
        {
            ModelState.AddModelError("Name", "Бренд с таким названием уже существует!");
        }

        if (ModelState.IsValid)
        {
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(category);
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
}