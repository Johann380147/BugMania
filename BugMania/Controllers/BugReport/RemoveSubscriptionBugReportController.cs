using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugMania.Entities;
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers.BugReport
{
    [RoutePrefix("Report/Subscribe")]
    public class RemoveSubscriptionBugReportController : Controller
    {
        BugReportEntity bugReportEntity = new BugReportEntity();
        ApplicationUserEntity userEntity = new ApplicationUserEntity();

        // GET: /Report/
        [Route("Remove")]
        public ActionResult RemoveSubscription(int id)
        {
            bugReportEntity.RemoveSubscriber(id, User.Identity.GetUserId());

            return Redirect("/Report/Details/" + id);
        }
    }
}