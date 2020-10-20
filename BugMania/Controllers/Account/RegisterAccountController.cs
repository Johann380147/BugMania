using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BugMania.Models;
using BugMania.Shapes;

namespace BugMania.Controllers.Account
{
    [RoutePrefix("Account")]
    public class RegisterAccountController : AccountController
    {
        //
        // GET: /Account/Register
        [Route("Register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("/Views/Account/Register.cshtml");
        }

        //
        // POST: /Account/Register
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsDeleted = false };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("ViewAllReports", "ViewAllBugReport");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View("/Views/Account/Register.cshtml", model);
        }
    }
}