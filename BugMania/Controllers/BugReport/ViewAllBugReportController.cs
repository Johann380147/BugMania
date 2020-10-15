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
    public class ViewAllBugReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private BugReportEntity bugReportEntity = new BugReportEntity();

        // GET: BugReport/Index
        // GET: BugReport/Index?filter=<SearchCriteria>
        [Route("View")]
        [Route("~/", Name = "Default")]
        public async Task<ActionResult> View(string filter)
        {
            if (String.IsNullOrEmpty(filter))
            {
                return View("/Views/BugReport/ViewAllBugReportUI.cshtml", await bugReportEntity.GetAllBugReports());
                
            }
            else
            {
                ViewBag.Filters = filter;
                return View("/Views/BugReport/ViewAllBugReportUI.cshtml", await bugReportEntity.GetFilteredBugReports(filter));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
