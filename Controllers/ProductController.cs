using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Controllers;

public class ProductController : Controller
{
    private ProductContext _context;

    public ProductController(ProductContext context)
    {
        _context = context;
    }

    
    public IActionResult Index(int? categoryId, int? brandId)
    {
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category).Include(p => p.Brand);

        if (categoryId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
        }

        if (brandId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.BrandId == brandId.Value);
        }

        var products = productsQuery.ToList();

        return View(products);
    }

    
    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (product != null)
        {
            product.DateOfCreation = DateTime.Now;
            _context.Add(product);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        List<Product> products = _context.Products.Include(p => p.Category).ToList();
        products = _context.Products.Include(p => p.Brand).ToList();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (product != null)
        {
            product.DateOfEditing = DateTime.Now;
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }
}