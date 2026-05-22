using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Product;

public class ListModel : PageModel
{
    private readonly IProductService _service;
    public List<ProductDto> Products { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }

    public ListModel(IProductService service)
    {
        Products = new List<ProductDto>();
        _service = service;
    }
    public async Task<IActionResult> OnGet()
    {
        //llamada al servicio
        var response = await _service.GetAllProducts(); 
        var products = response.Data;
        if (Id.HasValue )
        {
            products = products.Where(i=>i.Id == Id.Value).ToList();
        }
        Products = products;
        return Page();
    }
}