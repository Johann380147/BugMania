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
    public class ViewAllBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();

        [Route("View/All")]
        [Route("~/", Name = "Default")]
        public ActionResult ViewAllReports()
        {
            var model = bugReportEntity.GetSetOfReports(0, 10);
            return View("/Views/BugReport/ViewBugReportUI.cshtml", model);
        }

        public ActionResult FetchData(int skipCount, int takeCount, string filter)
        {
            var model = bugReportEntity.GetSetOfReports(skipCount, takeCount);
            
            if (model.Any())
            {
                return PartialView("/Views/BugReport/_ViewBugReportCardUI.cshtml", model);
            }
            else
            {
                return null;
            }
        }
    }
}
