﻿using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartsComponents;

public class _DashboardOrderStatusCartComponentPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _DashboardOrderStatusCartComponentPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var result = _context.Orders.GroupBy(o => o.Status).Select(g => new OrderStatusChartViewModel
        {
            Status = g.Key,
            Count = g.Count()
        }).ToList();
        return View(result);
    }
}
