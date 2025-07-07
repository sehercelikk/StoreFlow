using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers;

public class TodoController : Controller
{
    private readonly StoreContext _context;

    public TodoController(StoreContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateTodo()
    {
        var todos = new List<Todo>
        {
            new Todo{Description="Mail Gönder", Priority="Birincil", Status=true},
            new Todo{Description="Rapor Hazırla", Priority="İkincil", Status=true},
            new Todo{Description="Toplantıya Katol", Priority="Birincil", Status=false}
        };
        _context.Todos.AddRange(todos);
        _context.SaveChanges();
        return View();
    }

    public IActionResult TodoAgregatePriority()
    {
        var priorityFirstlyTodo=_context.Todos.Where(x=>x.Priority=="Birincil").Select(y=>y.Description).ToList();
        string result = priorityFirstlyTodo.Aggregate((acc, desc) => acc + " • " + desc);
        ViewBag.result = result;
        return View();
    }

    public IActionResult IncompleteTask()
    {
        var vlues = _context.Todos.Where(a => !a.Status).Select(b => b.Description).ToList().Prepend("Gün başında tüm görevleri kontrol etmeyi unutmayın").ToList();
        return View(vlues);
    }

    public IActionResult TodoChunk()
    {
        var values = _context.Todos.Where(c => !c.Status)
            .ToList()
            .Chunk(2).ToList();
        return View(values);
    }
    public IActionResult TodoConcat()
    {
        var values=_context.Todos.Where(a=>a.Priority=="Birincil")
            .ToList()
            .Concat(_context.Todos.Where(b=>b.Priority=="İkincil").ToList()).ToList();
        return View(values);
    }

    public IActionResult TodoUnion()
    {
        var values1 = _context.Todos.Where(a => a.Priority == "Birincil").ToList();
        var values2 = _context.Todos.Where(a => a.Priority == "İkincil").ToList();
        var result=values1.UnionBy(values2, x=>x.Description).ToList();
        return View(result);
    }
}
