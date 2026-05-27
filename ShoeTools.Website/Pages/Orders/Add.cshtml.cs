using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Orders;

public class AddModel : PageModel
{
  
    [BindProperty] public OrdersDto  OrderDto { get; set; }
    public List<ClientDto> clients { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IOrdersService  _ordersService;
    private readonly IClientService _clientService;

    public AddModel(IOrdersService ordersService, IClientService clientService)
    {
        _ordersService = ordersService;
        _clientService = clientService;
    }

    public async Task<IActionResult> OnGet()
    {
        clients = new List<ClientDto>();
        var response = await _clientService.GetAllClients();
        clients = response.Data;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Response<OrdersDto> response;
        OrderDto.Total = 0;
        OrderDto.OrderDate = DateTime.Now;
        OrderDto.PaymentStatus = false;
        response = await _ordersService.SaveAsync(OrderDto);

        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            foreach (var VARIABLE in Errors)
            {
                Console.WriteLine(VARIABLE);
            }

            return Page();
        }

        OrderDto = response.Data;
        return RedirectToPage("/Orders/List");
    }
}