using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Product;

public class DeleteModel : PageModel
{
    [BindProperty] public ProductDto Product { get; set; }
    private readonly IProductService _productService;

    public DeleteModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Product = new ProductDto();
        var response = await _productService.GetProductById(id);
        Product = response.Data;
        if(Product == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _productService.Delete(Product.Id);
        return RedirectToPage("./List");
    }
}