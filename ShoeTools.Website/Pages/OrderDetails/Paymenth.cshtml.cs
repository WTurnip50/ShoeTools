using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.OrderDetails;

public class Paymenth : PageModel
{
    [BindProperty]public OrdersDto OrdersDto { get; set; }
    public decimal total { get; set; }
    public List<OrderDetailsDto> OrderDetailsDtosList { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IOrdersService _orders;
    private readonly IOrderDetailsService _orderDetailsService;

    public Paymenth(IOrdersService orders, IOrderDetailsService orderDetailsService)
    {
        _orders = orders;
        _orderDetailsService = orderDetailsService;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        OrdersDto = new OrdersDto();
        if (id.HasValue)
        {
            var response = await _orders.GetOrderById(id.Value);
            OrdersDto = response.Data;
            var resDetails = await _orderDetailsService.GetAllDetails();
           
            var filteredDetails = resDetails.Data;
            if (resDetails != null)
            {
                filteredDetails = filteredDetails.Where(item => item.OrderID == OrdersDto.Id).ToList();
                OrderDetailsDtosList = filteredDetails;
            }

            foreach (var item in OrderDetailsDtosList)
            {
                total = total + (item.Quantity * item.UnitPrice);
            }
            
            OrdersDto.Total = total;
            OrdersDto.PaymentStatus = true;
        }
        if(OrdersDto == null)
        {
            return RedirectToPage("/Error");
        }

        if (OrdersDto.PaymentStatus)
        {
            return RedirectToPage("/Orders/List");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Campo: {state.Key} - Error: {error.ErrorMessage}");
                }
            }
            return Page();
        }
        
        OrdersDto.PaymentStatus = true;
        
        Response<OrdersDto> response;
        response = await  _orders.UpdateAsync(OrdersDto);
        
        Errors = response.Errors;
        if(Errors.Count > 0)
        {
            return Page();
        }
        OrdersDto = response.Data;
        return RedirectToPage("/Orders/List");
    }
}