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
    public class AddSubscriptionBugReportController : Controller
    {
        BugReportEntity bugReportEntity = new BugReportEntity();
        ApplicationUserEntity userEntity = new ApplicationUserEntity();

        // GET: /Report/
        [Route("Add")]
        public ActionResult AddSubscription(int id)
        {
            bugReportEntity.AddSubscriber(id, User.Identity.GetUserId());

            return Redirect("/Report/Details/" + id);
        }
    }
}