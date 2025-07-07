using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartsComponents;

public class _DashboardOrderDateCartComponentPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _DashboardOrderDateCartComponentPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var data = _context.Orders.GroupBy(a => a.OrderDate.Date)
            .Select(g => new
            {
                RawDate = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.RawDate).ToList()
            .Select(x => new OrderDateViewModel
            {
                Date = x.RawDate.ToString("yyyy-MM-dd"),
                Count = x.Count
            }).ToList();

        return View(data);
    }
}
