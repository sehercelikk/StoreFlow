using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.RightSidebarComponents;

public class _RightSidebarMessageComponentPartial: ViewComponent
{
    private readonly StoreContext _context;

    public _RightSidebarMessageComponentPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var result = _context.Messages.Where(a => a.IsRead == false).ToList();
        return View(result);
    }
}
