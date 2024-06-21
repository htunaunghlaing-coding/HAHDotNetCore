using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAHDotNetCore.PizzaApi.Db;
using HAHDotNetCore.PizzaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.PizzaApi.Features.Pizza;

[Route("api/[controller]")]
public class PizzaController : Controller
{
    private readonly AppDbContext _db;

    public PizzaController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public async Task<IActionResult> GetPizzaAsync()
    {
        var lst = await _db.Pizzas.ToListAsync();
        return Ok(lst);
    }

    [HttpGet("Extras")]
    public async Task<IActionResult> GetPizzaExtraAsync()
    {
        var lst = await _db.pizzaExtras.ToListAsync();
        return Ok(lst);
    }

    [HttpPost("Order")]
    public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
    {
        var PizzaItem = await _db.Pizzas.FirstOrDefaultAsync(x => x.PizzaId == orderRequest.PizzaId);
        var total = PizzaItem.Price;

        if (orderRequest.Extras.Length > 0)
        {
            var ExtraLists = await _db.pizzaExtras.Where(x => orderRequest.Extras.Contains(x.PizzaExtraId)).ToListAsync();
            total += ExtraLists.Sum(x => x.Price);
        }
        string invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
        PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
        {
            PizzaId = orderRequest.PizzaId,
            PizzaOrderInvoiceNo = invoiceNo,
            TotalAmount = total
        };

        List<PizzaOrderDetailModel> orderDetail = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel
        {
            PizzaExtraId = extraId,
            PizzaOrderInvoiceNo = invoiceNo
        }).ToList();

        await _db.pizzaOrderModels.AddAsync(pizzaOrderModel);
        await _db.pizzaOrderDetails.AddRangeAsync(orderDetail);
        await _db.SaveChangesAsync();

        OrderResponse orderResponse = new OrderResponse()
        {
            InvoiceNo = invoiceNo,
            TotalAmount = total,
            Message = "Thank you for your order!  Enjoy your Pizza."
        };

        return Ok();
    }

    [HttpGet("Order/{invoiceNo}")]
    public async Task<IActionResult> GetOrder(string invoiceNo)
    {
        var item = await _db.pizzaOrderModels.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
        var lst = await _db.pizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();

        var Order = new
        {
            Order = item,
            PizzaOrderDetailModel = lst
        };
        return Ok(Order);
    }
}
