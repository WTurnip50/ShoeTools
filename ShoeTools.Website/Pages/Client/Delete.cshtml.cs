using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Client;

public class DeleteModel : PageModel
{
    [BindProperty] public ClientDto Client { get; set; }
    private readonly IClientService _clientService;

    public DeleteModel(IClientService clientService)
    {
        _clientService = clientService;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        Client = new ClientDto();
        var response = await _clientService.GetClientById(id);
        Client = response.Data;
        if(Client == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _clientService.Delete(Client.Id);
        return RedirectToPage("./List");
    }
}