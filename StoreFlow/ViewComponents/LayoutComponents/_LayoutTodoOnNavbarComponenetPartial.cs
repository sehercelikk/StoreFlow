using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents;

public class _LayoutTodoOnNavbarComponenetPartial : ViewComponent
{
    private readonly StoreContext _context;

    public _LayoutTodoOnNavbarComponenetPartial(StoreContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var values = _context.Todos.Where(c=>c.Status==false).OrderBy(a => a.TodoId).Take(5).ToList();
        ViewBag.todoCount = _context.Todos.Count();
        return View(values);
    }
}
