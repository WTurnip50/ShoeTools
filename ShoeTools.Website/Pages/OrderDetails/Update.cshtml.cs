using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.OrderDetails;

public class Update : PageModel
{
    [BindProperty]public OrderDetailsDto OrderDetailsDto { get; set; }
    public ProductDetailsDto ProductDetailsDto { get; set; }
    [BindProperty]public List<ProductDetailsDto> ProductDetailsDtosList { get; set; }
    [BindProperty] public int idSize { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IProductDetailsService _service;
    private readonly IOrderDetailsService _orderDetailsService;

    public Update(IProductDetailsService service, IOrderDetailsService orderDetailsService)
    {
        _service = service;
        _orderDetailsService = orderDetailsService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        if (id > 0)
        {
            var response = await _orderDetailsService.GetDetailsById(id);
            OrderDetailsDto = response.Data;
            var list = await _service.GetAllProductDetails();
            var filter = list.Data;
            if (filter != null)
            {
                filter = filter.Where(item => item.IdProduct == response.Data.ProductID).ToList();
                Console.WriteLine("filtrando las tallas");
                ProductDetailsDtosList = filter;
            }
        }
        else
        {
            return NotFound();
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
            return RedirectToPage("/Orders/List");
        }
        
        var productDetailResponse = await _service.GetById(idSize);
        if (productDetailResponse?.Data != null)
        {
            OrderDetailsDto.UnitPrice = productDetailResponse.Data.Price;
            ProductDetailsDto = productDetailResponse.Data;
        }
        
        Response<OrderDetailsDto> response;
        response = await _orderDetailsService.UpdateAsync(OrderDetailsDto);
        Errors = response.Errors;
        if(Errors.Count > 0)
        {
            foreach (var error in Errors)
            {
                Console.WriteLine($"Error: {error}");
            }
            Console.WriteLine($"id: {OrderDetailsDto.Id},idProd:{OrderDetailsDto.ProductID}, idOrder:{OrderDetailsDto.OrderID}," +
                              $"quantity: {OrderDetailsDto.Quantity}, price: {OrderDetailsDto.UnitPrice}");
        }

        OrderDetailsDto = response.Data;
        return RedirectToPage("/Orders/List");
    }
}