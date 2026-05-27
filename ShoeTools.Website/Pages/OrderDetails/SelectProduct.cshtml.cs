using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.OrderDetails;

public class SelectProduct : PageModel
{
    [BindProperty]public OrderDetailsDto  OrderDetailsDto { get; set; }
    public List<ProductDto> Products { get; set; }
    public List<ProductDetailsDto> DetailsDto { get; set; }
    private readonly IProductService _productService;
    private readonly IProductDetailsService _productDetailsService;
    private readonly IOrderDetailsService _orderDetailsService;
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public SelectProduct(IProductService productService, IProductDetailsService productDetailsService, IOrderDetailsService orderDetailsService)
    {
        _productService = productService;
        _productDetailsService = productDetailsService;
        _orderDetailsService = orderDetailsService;
    }

    public async Task<IActionResult> OnGet()
    {
        Products = new List<ProductDto>();
        DetailsDto = new List<ProductDetailsDto>();
        var response = await _productService.GetAllProducts();
        var detailsResponse = await  _productDetailsService.GetAllProductDetails();
        if (response != null)
        {
            Products = response.Data;
        }
        else
        {
            return RedirectToPage("/Error", new { error = "No Products Found" });
        }

        if (detailsResponse != null)
        {
            DetailsDto = detailsResponse.Data;
        }
        else
        {
            return RedirectToPage("/Error", new { error = "No Details Found" });
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Id.HasValue)
        {
            OrderDetailsDto.OrderID = Id.Value;
            OrderDetailsDto.UnitPrice = 0;
            OrderDetailsDto.Quantity = 0;
        }

        Response<OrderDetailsDto> response;
        response = await _orderDetailsService.SaveAsync(OrderDetailsDto);
        OrderDetailsDto = response.Data;
        return Redirect($"/OrderDetails/List/{OrderDetailsDto.OrderID}");
    }
}