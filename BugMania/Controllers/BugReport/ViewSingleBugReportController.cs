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

namespace BugMania.BugReportControllers
{
    [RoutePrefix("Report")]
    public class ViewSingleBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();

        [Route("Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bugReport = await bugReportEntity.GetSingleBugReport(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }

            DetailsBugReportViewModel detailsModel = new DetailsBugReportViewModel(bugReport);

            return View("/Views/BugReport/ViewSingleBugReportUI.cshtml", detailsModel);
        }
    }
}
