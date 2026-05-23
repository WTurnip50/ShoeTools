using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Client;

public class List : PageModel
{
    private readonly IClientService _service;
    public List<ClientDto> Clients { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }

    public List(IClientService service)
    {
        _service = service;
        Clients = new List<ClientDto>();
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllClients();
        var clients = response.Data;
        if (Id.HasValue )
        {
            clients = clients.Where(i=>i.Id == Id.Value).ToList();
        }
        Clients = clients;
        return Page();
    }
}