using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents;

public class _Lats5ProjectComponentPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _Lats5ProjectComponentPartial(StoreContext context)
    {
        _context = context;
    }
    public IViewComponentResult Invoke()
    {
        var values= _context.Products.OrderBy(a=>a.ProductId).ToList().SkipLast(5).ToList().TakeLast(7).ToList();
        return View(values);
    }
}
