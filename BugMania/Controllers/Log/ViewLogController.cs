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

namespace BugMania.Controllers.Log
{
    [RoutePrefix("Log")]
    public class ViewLogController : Controller
    {
        LogEntity logEntity = new LogEntity();

        // GET: ViewLog
        [Route]
        [Route("View")]
        public ActionResult ViewAllLogs()
        {
            var weeklyLogs = logEntity.GetLogs(7);
            var monthlyLogs = logEntity.GetLogs(30);
            var allLogs = logEntity.GetAllLogs();

            var numberWeeklyNewReports = weeklyLogs
                .Where(o => o.Operation.Type == "CREATE")
                .Count();

            var numberWeeklyResolvedReports = weeklyLogs
                .Where(b => b.BugReport.Status.Name == "VERIFIED_FIXED")
                .GroupBy(i => i.BugReport.Id)
                .Count();

            var numberMonthlyNewReports = monthlyLogs
                .Where(o => o.Operation.Type == "CREATE")
                .Count();

            var numberMonthlyResolvedReports = monthlyLogs
                .Where(b => b.BugReport.Status.Name == "VERIFIED_FIXED")
                .GroupBy(i => i.BugReport.Id)
                .Count();

            var mostReportsFiled = allLogs
                .Where(o => o.Operation.Type == "CREATE")
                .GroupBy(i => i.EditorId)
                .OrderByDescending(c => c.Count())
                .Select(g => new LogCount { Log = g.FirstOrDefault(), Count = g.Count() } )
                .Take(3);

            var mostReportsResolved = allLogs
                .Where(o => o.Operation.Type == "EDIT" && 
                        o.Editor.Role.Name == "Developer" &&
                        o.Status == "UNVERIFIED_FIXED" &&
                        o.BugReport.Status.Name == "VERIFIED_FIXED")
                .GroupBy(i => i.EditorId)
                .OrderByDescending(c => c.Count())
                .Select(g => new LogCount { Log = g.FirstOrDefault(), Count = g.Count() })
                .Take(3);

            var mostReportsByReporter = allLogs
                .Where(o => o.Operation.Type == "CREATE" &&
                        o.Editor.Role.Name == "Reporter")
                .GroupBy(i => i.EditorId)
                .OrderByDescending(c => c.Count())
                .Select(g => new LogCount { Log = g.FirstOrDefault(), Count = g.Count() })
                .Take(3);

            mostReportsFiled = CollectionHelpers.ReorderChampionList(mostReportsFiled);
            mostReportsResolved = CollectionHelpers.ReorderChampionList(mostReportsResolved);
            mostReportsByReporter = CollectionHelpers.ReorderChampionList(mostReportsByReporter);

            LogViewModel logViewModel = new LogViewModel()
            {
                numberWeeklyNewReports = numberWeeklyNewReports,
                numberWeeklyResolvedReports = numberWeeklyResolvedReports,
                numberMonthlyNewReports = numberMonthlyNewReports,
                numberMonthlyResolvedReports = numberMonthlyResolvedReports,
                TopThreeReported = mostReportsFiled,
                TopThreeDeveloper = mostReportsResolved,
                TopThreeReporter = mostReportsByReporter
            };

            return View("/Views/Log/ViewAllLogUI.cshtml", logViewModel);
        }
    }
}