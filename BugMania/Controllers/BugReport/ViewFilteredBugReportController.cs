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
    public class ViewFilteredBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();
        
        [Route("View")]
        public ActionResult ViewFilteredReports(string filter)
        {
            ViewBag.Filters = filter;
            var model = bugReportEntity.GetFilteredSetOfReports(0, 10, filter);
            return View("/Views/BugReport/ViewBugReportUI.cshtml", model);
        }

        public ActionResult FetchFilteredData(int skipCount, int takeCount, string filter)
        {   
            var model = bugReportEntity.GetFilteredSetOfReports(skipCount, takeCount, filter);

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