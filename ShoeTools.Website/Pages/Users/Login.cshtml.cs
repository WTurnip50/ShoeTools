using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;
using ShoeTools.Website.Services.Interfaces;

namespace ShoeTools.Website.Pages.Users;

public class LoginModel : PageModel
{
    private IUserService _userService;
    public List<string> Errors { get; set; } = new List<string>();
    [BindProperty] public AppUsersDto User { get; set; }

    public LoginModel(IUserService userService)
    {
        _userService = userService;
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return Page();
        }
        Response<AppUsersDto> response;
        response = await _userService.LogIn(User);
        
        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            return Page();
        }
        User = response.Data;
        return RedirectToPage("/Product/List");
    }
}