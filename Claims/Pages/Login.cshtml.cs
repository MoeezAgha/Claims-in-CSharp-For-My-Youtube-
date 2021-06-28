using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Claims.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Credential Credential { get; set; }
        //mvvm but it is mvc 


        public void OnGet()
        {
            this.Credential = new Credential { UserName = "admin" };
        }

        public async Task <IActionResult> OnPost()
        {
            var cookieName = "MrCookiesAuth";
            if (!ModelState.IsValid) return Page();

            if (Credential.UserName == "admin" && Credential.Password == "agha")
            {
               var claims = new List<Claim> {
               new Claim(ClaimTypes.Name, "admin"),
               new Claim(ClaimTypes.Email, "admin@moeez.com"),
               new Claim("Department", "HR"),
                  new Claim(ClaimTypes.Role, "ddagha")

               };
                
              
                //MustHaveAgha

                var identity = new ClaimsIdentity(claims, cookieName);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

              
                //RememberMe
                //var authProperties = new AuthenticationProperties();
                //authProperties.IsPersistent = Credential.RememberMe;
                //await HttpContext.SignInAsync(cookieName, claimsPrincipal,authProperties);

                await  HttpContext.SignInAsync(cookieName, claimsPrincipal);

                return RedirectToPage("/Index");
            }



            return Page();

        }





    }
    

    public class Credential { 
    
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } //after


    }
}
