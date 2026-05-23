using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.ProductDetails;

public class DeleteModel : PageModel
{
    [BindProperty] public ProductDetailsDto ProductDetailsDto { get; set; }
    private readonly IProductDetailsService _productDetailsService;

    public DeleteModel(IProductDetailsService productDetailsService)
    {
        _productDetailsService = productDetailsService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        ProductDetailsDto = new ProductDetailsDto();
        var response = await _productDetailsService.GetById(id);
        ProductDetailsDto = response.Data;
        if(ProductDetailsDto == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _productDetailsService.DeleteAsync(ProductDetailsDto.Id);
        return RedirectToPage($"/Product/List");
    }
}