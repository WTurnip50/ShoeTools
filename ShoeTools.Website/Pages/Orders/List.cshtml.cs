using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Orders;

public class List : PageModel
{
    private readonly IOrdersService _ordersService;
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    public List<OrdersDto>  Orders { get; set; }

    public List(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    public async Task<IActionResult> OnGet()
    {
        Orders = new List<OrdersDto>();
        var response =await _ordersService.GetAllOrders();
        Orders = response.Data;
        return Page();
    }
}