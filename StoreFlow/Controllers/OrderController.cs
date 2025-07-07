using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers;

public class OrderController : Controller
{
    private readonly StoreContext _context;

    public OrderController(StoreContext context)
    {
        _context = context;
    }

    public IActionResult AllStokSmallerThen5()
    {
        var result = _context.Orders.All(x => x.Count <= 5);
        if(result)
        {
            ViewBag.v = "Tüm siparişler 5 adetten küçük";
        }
        else
        {
            ViewBag.v = "Tüm siparişler 5 adetten küçük değil";

        }
        return View();
    }

    public IActionResult OrderListByStatus(string status)
    {
        var values= _context.Orders.Where(x=>x.Status.Contains(status)).ToList();
        if (!values.Any())  
            ViewBag.v = "Bu status ile ilgili veri bulunamadı";
        return View(values);
    }

    public IActionResult OrderListSearch(string name, string filterType)
    {
        if(filterType=="start")
        {
            var values= _context.Orders.Where(x=>x.Status.StartsWith(name)).ToList();
            return View(values);
        }
        else if(filterType=="end")
        {
            var values= _context.Orders.Where(a=>a.Status.EndsWith(name)).ToList();
            return View(values);
        }
        var orderValues = _context.Orders.ToList();
        return View(orderValues);
    }

    public async Task<IActionResult> OrderAsyncList()
    {
        var result = await _context.Orders.Include(x=>x.Product).Include(v=>v.Customer).ToListAsync();
        return View(result);
    }


    [HttpGet]
    public async Task<IActionResult> CreateOrder()
    {
        var product = await _context.Products.Select(a => new SelectListItem
        {
           Value=a.ProductId.ToString(),
           Text=a.Name,
        }).ToListAsync();
        ViewBag.product = product;
        var customer= await _context.Customers.Select(b=> new SelectListItem
        {
            Value=b.CustomerId.ToString(),
            Text=b.Name+ " "+ b.Surname
            
        }).ToListAsync();
        ViewBag.customer = customer;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        order.Status = "Sipariş Alındı";
        order.OrderDate = DateTime.Now;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return RedirectToAction("OrderAsyncList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateOrder(int id)
    {
        var product = await _context.Products.Select(a => new SelectListItem
        {
            Value = a.ProductId.ToString(),
            Text = a.Name,
        }).ToListAsync();
        ViewBag.product = product;
        var customer = await _context.Customers.Select(b => new SelectListItem
        {
            Value = b.CustomerId.ToString(),
            Text = b.Name + " " + b.Surname

        }).ToListAsync();
        ViewBag.customer = customer;

        var findId= await _context.Orders.FindAsync(id);
        return View(findId);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrder(Order order)
    {
        var values= _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return RedirectToAction("OrderAsyncList");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var value = await _context.Orders.FindAsync(id);
         _context.Orders.Remove(value);
        await _context.SaveChangesAsync();
        return RedirectToAction("OrderAsyncList");
    }

    public IActionResult OrderListWithCustomerGroup()
    {
        var result = from customer in _context.Customers
                     join order in _context.Orders
                     on customer.CustomerId equals order.CustomerId
                     into orderGroup
                     select new CustomerOrderViewModel
                     {
                         CustomerName = customer.Name,
                         Orders = orderGroup.ToList()
                     };
        return View(result.ToList());


    }
}
