using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers;

public class CustomerController : Controller
{
    private readonly StoreContext _context;

    public CustomerController(StoreContext context)
    {
        _context = context;
    }

    public IActionResult CustomerList()
    {
        var values = _context.Customers.OrderBy(a=>a.Name).ToList();
        return View(values);
    }
    public IActionResult CustomerListBalanceDesc()
    {
        var values = _context.Customers.OrderByDescending(a => a.Balance).ToList();
        return View(values);
    }

    public async Task<IActionResult> CustomerGetByCity(string city)
    {
        var result= await _context.Customers.AnyAsync(a=>a.City==city);
        if(result)
        {
            ViewBag.Message = $"{city} şehrinden en az bir tane müşteri var";
        }
        else
        {
            ViewBag.Message = $"{city} şehrinden hiç müşteri yok";

        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateCustomer() => View();

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction("CustomerList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCustomer(int id)
    {
        var findCustomer = await _context.Customers.FindAsync(id);
        return View(findCustomer);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCustomer(Customer customer)
    {
        var updated = _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction("CustomerList");
    }

    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var findCustomer = await _context.Customers.FindAsync(id);
        _context.Customers.Remove(findCustomer);
        await _context.SaveChangesAsync();
        return RedirectToAction("CustomerList");
    }

    public IActionResult CustomerListByCity()
    {
        var groupCustomer= _context.Customers.GroupBy(g => g.City).ToList();
        return View(groupCustomer);
    }
    public IActionResult CustomerByCityCount()
    {
        var query = from c in _context.Customers
                    group c by c.City into cityGroup
                    select new CustomerCityGroup
                    {
                        City = cityGroup.Key,
                        Count = cityGroup.Count()
                    };
        var model= query.ToList();
        return View(model);
    }

    public IActionResult CustomerCityDistinct()
    {
        var values = _context.Customers.Select(a => a.City).Distinct().ToList();
        return View(values);
    }

    public IActionResult ParallelCustomers()
    {
        var customers = _context.Customers.ToList();
        var result = customers.AsParallel()
            .Where(c => c.City.StartsWith("A", StringComparison.OrdinalIgnoreCase)).ToList();
        return View(result);
    }

    public IActionResult CustomerListCityExceptIstanbul()
    {
        var  allCustomers=_context.Customers.ToList();
        var customersListInIstanbul = _context.Customers.Where(a => a.City == "Ankara").Select(s=>s.City).ToList();
        var result= allCustomers.ExceptBy(customersListInIstanbul, x=>x.City).ToList();
        return View(result);
    }


    public IActionResult CustomerListWithDefaultIfEmpty()
    {
        var customer = _context.Customers.Where(a => a.City == "Şırnak").ToList().DefaultIfEmpty(new Customer
        {
            CustomerId = 0,
            Name = "Kayıt Yok",
            Surname = "--",
            City = "Şırnak",
        }).ToList();
        return View(customer);
    }

    public IActionResult CustomerIntersectByCiy()
    {
        var cityValues1 = _context.Customers.Where(b => b.City == "Antalya").Select(y => y.Name + " " + y.Surname).ToList();
        var cityValues2 = _context.Customers.Where(b => b.City == "Ankara").Select(y => y.Name + " " + y.Surname).ToList();

        var result= cityValues1.Intersect(cityValues2).ToList();
        return View(result);
    }

    public IActionResult CustomerCastExemple()
    {
        var values = _context.Customers.ToList();
        ViewBag.v = values;
        return View();
    }

    public IActionResult CustomerListWithIndex()
    {
        var result= _context.Customers.ToList().Select((c,index)=>new
        {
            SiraNo= index+1,
            c.Name,
            c.Surname,
            c.City,
        }).ToList();
        return View(result);
    }
}
