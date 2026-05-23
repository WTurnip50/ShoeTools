using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.ProductDetails;

public class EditModel : PageModel
{
    [BindProperty] public ProductDetailsDto ProductDetailsDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    //[BindProperty(SupportsGet = true)]
    //public int? IdProdcut { get; set; }
    
    private readonly IProductService _productService;
    private readonly IProductDetailsService _productDetailsService;

    public EditModel(IProductService productService, IProductDetailsService productDetailsService)
    {
        _productService = productService;
        _productDetailsService = productDetailsService;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        ProductDetailsDto = new ProductDetailsDto();
        if (id.HasValue)
        {
            var response = await _productDetailsService.GetById(id.Value);
            Console.WriteLine($"Registro a modificar : {response.Data.Id}");
            ProductDetailsDto = response.Data;
        }
        if(ProductDetailsDto == null)
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

        Response<ProductDetailsDto> response;
        if (ProductDetailsDto.Id > 0)
        {
            response = await _productDetailsService.UpdateAsync(ProductDetailsDto);
        }
        else
        {
            response = await _productDetailsService.SaveAsync(ProductDetailsDto);
        }
        Errors = response.Errors;
        if(Errors.Count > 0)
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