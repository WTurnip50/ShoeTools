using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.ProductDetails;

public class List : PageModel
{
    public List<ProductDetailsDto>  productDetails;
    [BindProperty] public ProductDto Product { get; set; }
    private readonly IProductService _ProductService;
    private readonly IProductDetailsService _ProductDetailsService;
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }

    public List(IProductService productService, IProductDetailsService productDetailsService)
    {
        productDetails = new List<ProductDetailsDto>();
        _ProductService = productService;
        _ProductDetailsService = productDetailsService;
    }

    public async Task<IActionResult> OnGet()
    {
        Product = new ProductDto();
        var response = await _ProductDetailsService.GetAllProductDetails();
        var filteredProduct = response.Data;
        if (Id.HasValue)
        {
            var res = await _ProductService.GetProductById(Id.Value);
            Product = res.Data;
            Console.WriteLine("Id Producto obtenido"+Product.Id);
            filteredProduct = filteredProduct.Where(i=>i.IdProduct == Product.Id).ToList();
            foreach (var item in filteredProduct)
            {
                Console.WriteLine("Id Producto Filtrado : "+item.Id);
            }
        }
        productDetails = filteredProduct;
        return Page();
    }
}