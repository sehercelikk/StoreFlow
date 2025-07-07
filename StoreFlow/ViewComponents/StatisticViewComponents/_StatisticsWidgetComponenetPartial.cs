using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.StatisticViewComponents;

public class _StatisticsWidgetComponenetPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _StatisticsWidgetComponenetPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.totalCategoryCount = _context.Categories.Count();
        ViewBag.productMaxPrice = _context.Products.Max(a => a.Price);
        ViewBag.ProductMaxName = _context.Products.Where(a => a.Price == (_context.Products.Max(c => c.Price))).Select(b => b.Name).FirstOrDefault();
        ViewBag.productMinPrice = _context.Products.Min(a => a.Price);
        ViewBag.ProductMinName = _context.Products.Where(a => a.Price==(_context.Products.Min(x=>x.Price))).Select(n=>n.Name).FirstOrDefault();
        ViewBag.totalSumProductCount = _context.Products.Sum(a => a.Stok);
        ViewBag.awgProductStok=_context.Products.Average(a => a.Stok);
        ViewBag.awgProductPrice=_context.Products.Average(a => a.Price);
        ViewBag.priceBiggerThen1000Product = _context.Products.Where(a => a.Price > 1000).Count();
        ViewBag.idProduct = _context.Products.Where(a => a.ProductId == 4).Select(c=>c.Name).FirstOrDefault();
        ViewBag.stokCount50ve100arasıolanlar=_context.Products.Where(v=>v.Stok>50 && v.Stok< 100).Count();
        return View();
    }
}
