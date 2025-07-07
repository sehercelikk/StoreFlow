using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents;

public class _LayoutMesasageOnNavbarComponentPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _LayoutMesasageOnNavbarComponentPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var message= _context.Messages.Where(a=>a.IsRead==false).OrderByDescending(b=>b.MessageId).Take(3).ToList();
        ViewBag.totalMessage = _context.Messages.Where(a=>a.IsRead==false).Count();
        return View(message);
    }
}
