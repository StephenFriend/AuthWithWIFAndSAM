using System;
using System.Collections.Generic;
using System.Linq;
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
               FormsAuthentication.SetAuthCookie(model.Email, false);
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
    }
}
