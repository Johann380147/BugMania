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

namespace BugMania.Controllers
{
    [Authorize]
    public class BugReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private BugReportEntity bugReportEntity = new BugReportEntity();

        // GET: BugReport/Index
        // GET: BugReport/Index?filter=<SearchCriteria>
        [AllowAnonymous]
        public async Task<ActionResult> ViewBugReports(string filter)
        {
            if (String.IsNullOrEmpty(filter))
            {
                return View(await bugReportEntity.GetAllBugReports());
                
            }
            else
            {
                ViewBag.Filters = filter;
                return View(await bugReportEntity.GetFilteredBugReports(filter));
            }
        }

        // GET: BugReport/Details/5
        [AllowAnonymous]
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

            return View(detailsModel);
        }

        // GET: BugReport/Create
        public ActionResult Create()
        {
            CreateBugReportViewModel model = new CreateBugReportViewModel();

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");
            return View(model);
        }

        // POST: BugReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBugReportViewModel createBugReportViewModel)
        {
            if (ModelState.IsValid)
            {
                bugReportEntity.AddBugReport(createBugReportViewModel);
                
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", createBugReportViewModel.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", createBugReportViewModel.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", createBugReportViewModel.SeverityId);
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");

            return View(createBugReportViewModel);
        }

        // GET: BugReport/Edit/5
        public async Task<ActionResult> Edit(int id)
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

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", bugReport.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", bugReport.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", bugReport.SeverityId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", bugReport.StatusId);

            return View(bugReport);
        }

        // POST: BugReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ProductId,AuthorId,SeverityId,PriorityId,StatusId,CreateDateTime")] BugReport bugReport)
        {
            if (ModelState.IsValid)
            {
                bugReportEntity.UpdateBugReport(bugReport);
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", bugReport.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", bugReport.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", bugReport.SeverityId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", bugReport.StatusId);

            return View(bugReport);
        }

        // GET: BugReport/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = await db.BugReports.FindAsync(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }
            return View(bugReport);
        }

        // POST: BugReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BugReport bugReport = await db.BugReports.FindAsync(id);
            db.BugReports.Remove(bugReport);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
