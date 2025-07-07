using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents;

public class _CardStatisticsComponentDashboardPartial: ViewComponent
{
    private readonly StoreContext _context;
    public _CardStatisticsComponentDashboardPartial(StoreContext storeContext)
    {
        _context = storeContext;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.totalCustomerCount = _context.Customers.Count();
        ViewBag.totalCategoryCount=_context.Categories.Count();
        ViewBag.totalProductCount=_context.Products.Count();

        ViewBag.avgCustomerBalance = _context.Customers.Average(a => a.Balance).ToString("N2") + " TL";
        ViewBag.totalOrderCount = _context.Orders.Count();
        ViewBag.sumOrderProductCount = _context.Orders.Sum(x => x.Count);
        return View();
    }
}
