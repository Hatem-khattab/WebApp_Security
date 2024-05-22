using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Webappunderthehood.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential {  get; set; }  = new Credential();
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (credential.UserName == "Hkdr" && credential.Password == "12345") 
            {
                var cliams = new List<Claim> {
                  new Claim (ClaimTypes.Name, "admin"),
                  new Claim (ClaimTypes.Email,"Admin@Gmail.com")
                  };

                var Identity = new ClaimsIdentity (cliams, "myCookieAuth" );
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal (Identity);

                await HttpContext.SignInAsync("myCookieAuth", claimsPrincipal);
                 return RedirectToPage("/Index");

            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name ="User Name")]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
