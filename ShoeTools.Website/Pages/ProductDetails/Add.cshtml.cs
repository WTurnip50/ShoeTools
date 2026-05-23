using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.ProductDetails;

public class AddModel : PageModel
{
    [BindProperty] public ProductDetailsDto ProductDetailsDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IProductDetailsService _productDetailsService;

    public AddModel(IProductDetailsService productDetailsService)
    {
        _productDetailsService = productDetailsService;
    }

    public void OnGet(int id)
    {
       
            ProductDetailsDto = new ProductDetailsDto{IdProduct =  id};
            Console.WriteLine($"Id Product : {ProductDetailsDto.IdProduct}");
       
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<ProductDetailsDto> response;
        response = await _productDetailsService.SaveAsync(ProductDetailsDto);

        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            foreach (var VARIABLE in Errors)
            {
                Console.WriteLine(VARIABLE);
            }

            return Page();
        }

        ProductDetailsDto = response.Data;
        return RedirectToPage("/Product/List");
    }
}