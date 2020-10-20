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
    public class LogOffAccountController : AccountController
    {
        //
        // POST: /Account/LogOff
        [Route("LogOff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("ViewAllReports", "ViewAllBugReport");
        }
    }
}