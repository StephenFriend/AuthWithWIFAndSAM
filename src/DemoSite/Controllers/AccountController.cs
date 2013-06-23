using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DemoSite.Models;

namespace DemoSite.Controllers
{
    public class AccountController : Controller
    {
       public ActionResult Login()
       {
           return View();
       }

       [HttpPost]
       public ActionResult Login(LoginInput model)
       {
           if (ModelState.IsValid && LoginDetailsAreValid(model))
           {
               WriteAuthCookie(model.Email);
               return RedirectToAction("MyStuff", "Home");
           }

           ModelState.AddModelError("", "The user name or password provided is incorrect.");
           return View(model);
       }

        // In the real world this would be most likely be a call to a database to get the user details, with validation 
        // that the user exists and that their password is correct.  See WebMatrix.WebData.SimpleMembershipProvider.ValidateUser()
        // for an example (download the ASP.Net web stack here: http://aspnetwebstack.codeplex.com/)
        private bool LoginDetailsAreValid(LoginInput loginDetails)
        {
            return (string.Compare(loginDetails.Email, "you@example.com",
                                   StringComparison.InvariantCultureIgnoreCase) == 0 &&
                    string.Compare(loginDetails.Password, "password", StringComparison.Ordinal) == 0);
        }

        private void WriteAuthCookie(string userEmail)
        {
            var claims = new List<Claim>();
            claims.Insert(0, new Claim(ClaimTypes.Name, userEmail)); 
            var claimsId = new ClaimsIdentity(claims, "Password");
            var cp = new ClaimsPrincipal(claimsId);
            var sam = FederatedAuthentication.SessionAuthenticationModule;
            var token = new SessionSecurityToken(cp);

            sam.WriteSessionTokenToCookie(token);
        }
    }
}
