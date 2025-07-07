using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers;

public class ProductController : Controller
{
   private readonly StoreContext _context;

    public ProductController(StoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ProductList()
    {
        var result =await _context.Products.Include(a=>a.Category).ToListAsync();
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var category =await _context.Categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.Name,
        }).ToListAsync() ;
        ViewBag.category= category;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("ProductList");
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("ProductList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var category = await _context.Categories.Select(a => new SelectListItem
        {
            Value = a.CategoryId.ToString(),
            Text = a.Name,
        }).ToListAsync();
        var value = await _context.Products.FindAsync(id);
        await _context.SaveChangesAsync();
        ViewBag.category= category;
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("ProductList");

    }

    public IActionResult Last5List()
    {
        var result= _context.Products.Include(a=>a.Category).Take(5).ToList();
        return View(result);
    }

    public IActionResult Skip4ProductList()
    {
       var values= _context.Products.Include(a=>a.Category).Skip(4).Take(10).ToList();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateProductWithAttach() => View();

    [HttpPost]
    public IActionResult CreateProductWithAttach(Product product)
    {
        var category = new Category { CategoryId = 1 };
        _context.Categories.Attach(category);
        var productValue = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Stok = product.Stok,
            Category = category
        };
        _context.Products.Add(productValue);
        _context.SaveChanges();
        return RedirectToAction("ProductList");
    }

    public IActionResult ProductCount()
    {
        var value = _context.Products.LongCount();
        var lastProduct = _context.Products.OrderBy(a=>a.ProductId).Last();
        ViewBag.v1 = lastProduct.Name;
        ViewBag.v=value;
        return View();
    }

    public IActionResult ProductListWithCategory()
    {
        var query = from p in _context.Products
                    join c in _context.Categories
                    on p.CategoryId equals c.CategoryId
                    select new ProductWithCategoryViewModel
                    {
                        ProductName=p.Name,
                        Stock=p.Stok,
                        CategoryName= c.Name
                    };
        return View(query.ToList());
    }
}
