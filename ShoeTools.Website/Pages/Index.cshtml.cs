using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoeTools.Website.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        Response.Redirect("/Users/login");
    }
}