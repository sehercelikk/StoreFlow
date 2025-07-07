using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents;

public class _ChartdashBoardComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
