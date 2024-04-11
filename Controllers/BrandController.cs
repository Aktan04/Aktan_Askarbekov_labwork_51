using Microsoft.AspNetCore.Mvc;
using ProductShop.Models;

namespace ProductShop.Controllers;

public class BrandController : Controller
{
    private ProductContext _context;

    public BrandController(ProductContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var brands = _context.Brands.ToList();
        return View(brands);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Brand brand)
    {
        if (string.IsNullOrEmpty(brand.NameOfBrand))
        {
            ModelState.AddModelError("Name", "Заполните название бренда!");
        }
        else if (_context.Brands.Any(b => b.NameOfBrand.ToLower().Trim() == brand.NameOfBrand.ToLower().Trim()))
        {
            ModelState.AddModelError("Name", "Бренд с таким названием уже существует!");
        }

        if (ModelState.IsValid)
        {
            _context.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(brand);
    }
    
    
    public IActionResult Delete(int id)
    {
        var brand = _context.Brands.Find(id);
        if (brand == null)
        {
            return NotFound();
        }
        _context.Brands.Remove(brand);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
}