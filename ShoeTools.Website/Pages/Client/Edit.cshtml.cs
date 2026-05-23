using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Client;

public class Edit : PageModel
{
    [BindProperty] public ClientDto Client { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IClientService _service;

    public Edit(IClientService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id )
    {
        Client = new ClientDto();
        if (id.HasValue)
        {
            var response = await _service.GetClientById(id.Value);
            Client = response.Data;
        }

        if (Client == null)
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
        Response<ClientDto> response;
        if (Client.Id > 0)
        {
            response = await _service.UpdateAsync(Client);
        }
        else
        {
            response = await _service.SaveAsync(Client);
        }
        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            return Page();
        }

        Client = response.Data;
        return RedirectToPage("./List");
    }
}