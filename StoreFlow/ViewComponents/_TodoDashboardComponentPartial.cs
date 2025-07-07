﻿using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class _TodoDashboardComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;

        public _TodoDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Todos.OrderByDescending(a => a.TodoId).Take(6).ToList();
            return View(values);
        }
    }
}
