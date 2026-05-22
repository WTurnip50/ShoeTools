using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Product;

public class EditModel : PageModel
{
    [BindProperty] public ProductDto Product { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IProductService _service;
    
    public EditModel(IProductService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int? id)
    {
        Product = new ProductDto();
        if(id.HasValue)
        {
            var response = await _service.GetProductById(id.Value);
            Product = response.Data;
        }
        if(Product == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<ProductDto> response;
        
        if(Product.Id > 0)
        {
            response = await _service.UpdateAsync(Product);
        }else
        {
            response = await _service.SaveAsync(Product);
        }
        
        Errors = response.Errors;
        if(Errors.Count > 0)
        {
            return Page();
        }
        Product = response.Data;
        return RedirectToPage("/Product/List");
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage("/Product/List");
    }
}