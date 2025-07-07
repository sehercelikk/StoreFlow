using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using System.Threading.Tasks;

namespace StoreFlow.Controllers;

public class CategoriesController : Controller
{
    private readonly StoreContext _context;
    public CategoriesController(StoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> CategoryList()
    {
        var result = await _context.Categories.ToListAsync();
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory() => View();

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category model)
    {
        model.Status = false;
        await _context.Categories.AddAsync(model);
        _context.SaveChanges();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var result = await _context.Categories.FindAsync(id);
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var value = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(value);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");

    }

    public async Task<IActionResult> ReverseCategory()
    {
        var categoryValue = _context.Categories.First();
        ViewBag.v=categoryValue.Name;

        var singleOrDefaultCategory = _context.Categories.SingleOrDefault(a=>a.Name=="Anne ve Bebek Ürünleri");
        ViewBag.v2 = singleOrDefaultCategory.Status +" "+ singleOrDefaultCategory.CategoryId.ToString() ;

        var values =await _context.Categories.OrderBy
            (a=>a.CategoryId).Reverse().ToListAsync();
        return View(values);
    }
}