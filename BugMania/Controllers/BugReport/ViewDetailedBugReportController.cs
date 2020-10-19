using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugMania.DataContexts;
using BugMania.Shapes;
using Microsoft.AspNet.Identity;
using BugMania.Models;
using BugMania.Helpers;
using BugMania.Entities;

namespace BugMania.Controllers.BugReport
{
    [RoutePrefix("Report")]
    public class ViewDetailedBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();

        [Route("Details/{id}")]
        public ActionResult ViewDetailedReport(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bugReport = bugReportEntity.GetSingleBugReport(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }

            DetailsBugReportViewModel detailsModel = new DetailsBugReportViewModel(bugReport);

            return View("/Views/BugReport/ViewDetailedBugReportUI.cshtml", detailsModel);
        }
    }
}
