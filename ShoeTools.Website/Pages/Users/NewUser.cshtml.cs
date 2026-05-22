using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Users;

public class NewUserModel : PageModel
{
    private IUserService _userService;
    public List<string> Errors { get; set; } = new List<string>();
    [BindProperty] public AppUsersDto User { get; set; }

    public NewUserModel(IUserService userService)
    {
        _userService = userService;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Campo: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
            return Page();
        }
        Response<AppUsersDto> response;
        response = await _userService.SaveAsync(User);
        Errors = response.Errors;
        if(Errors.Count > 0)
        {
            Console.WriteLine(Errors.Count);
            return RedirectToPage("/Error");
        }
        User = response.Data;
        return RedirectToPage("./Login");
    }
}