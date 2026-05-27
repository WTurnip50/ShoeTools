using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.OrderDetails;

public class List : PageModel
{
    public List<OrderDetailsDto> DetailsDto { get; set; }
    public List<ProductDto> ProductDtos { get; set; }
    private IOrderDetailsService _orderDetailsService;
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }

    public List(IOrderDetailsService orderDetailsService)
    {
        _orderDetailsService = orderDetailsService;
    }

    public async Task<IActionResult> OnGet()
    {
        DetailsDto = new List<OrderDetailsDto>();
        var response = await _orderDetailsService.GetAllDetails();
        var filteredDetails = response.Data;
        if (Id.HasValue)
        {
            filteredDetails = filteredDetails.Where(id=>id.OrderID == Id.Value).ToList();
            DetailsDto = filteredDetails;
        }
        return Page();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // Llamas al servicio para eliminar el detalle de la orden
        await _orderDetailsService.Delete(id);

        // Rediriges de nuevo a la misma página para refrescar la lista
        return RedirectToPage("./List", new { Id = this.Id });
    }
}