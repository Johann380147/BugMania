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
    [Authorize(Roles = "Admin, Triager")]
    public class DeleteBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();

        // GET: BugReport/Delete/5
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = await bugReportEntity.GetSingleBugReport(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }
            return View("/Views/BugReport/DeleteBugReportUI.cshtml",bugReport);
        }

        // POST: BugReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bugReportEntity.DeleteBugReport(id);
            return RedirectToAction("View", "ViewAllBugReport");
        }
    }
}
