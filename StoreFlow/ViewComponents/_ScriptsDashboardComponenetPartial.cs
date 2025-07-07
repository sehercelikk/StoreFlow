using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents;

public class _ScriptsDashboardComponenetPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
