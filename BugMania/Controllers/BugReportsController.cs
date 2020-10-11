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
using Reports.Entities;
using Microsoft.AspNet.Identity;
using BugMania.Models;
using BugMania.Helpers;

namespace BugMania.Controllers
{
    public class BugReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BugReports/<SearchCriteria>
        public async Task<ActionResult> Index(string filter)
        {
            // Find - gets local, SingleOrDefault - force get from DB
            if (!String.IsNullOrEmpty(filter))
            {
                var bugReports = db.BugReports.Where(
                    b => b.Title.Contains(filter) ||
                    b.Description.Contains(filter) ||
                    b.Tags.Any(t => t.Name.Contains(filter)))
                    .Include(b => b.Priority)
                    .Include(b => b.Product)
                    .Include(b => b.Severity)
                    .Include(b => b.Status)
                    .Include(b => b.Tags);
                return View(await bugReports.ToListAsync());
            }
            else
            {
                var bugReports = db.BugReports
                    .Include(b => b.Priority)
                    .Include(b => b.Product)
                    .Include(b => b.Severity)
                    .Include(b => b.Status)
                    .Include(b => b.Tags);
                return View(await bugReports.ToListAsync());
            }
        }

        // GET: BugReports/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = db.BugReports
                                    .Include(b => b.Product)
                                    .Include(b => b.Author)
                                    .Include(b => b.Severity)
                                    .Include(b => b.Priority)
                                    .Include(b => b.Status)
                                    .Include(b => b.Tags)
                                    .Include(b => b.Comments)
                                    .Include(b => b.Assignees)
                                    .SingleOrDefault(b => b.Id == id);
            
            if (bugReport == null)
            {
                return HttpNotFound();
            }

            DetailsBugReportViewModel detailsModel = new DetailsBugReportViewModel(bugReport);

            return View(detailsModel);
        }

        // GET: BugReports/Create
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

        // POST: BugReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Description,ProductId,SeverityId,PriorityId,Tags")] CreateBugReportViewModel createBugReportModel)
        {
            if (ModelState.IsValid)
            {
                BugReport bugReport = new BugReport();
                bugReport.AuthorId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                bugReport.CreateDateTime = DateTime.Now;
                bugReport.Title = createBugReportModel.Title;
                bugReport.Description = createBugReportModel.Description;
                bugReport.ProductId = createBugReportModel.ProductId;
                bugReport.SeverityId= createBugReportModel.SeverityId;
                bugReport.PriorityId= createBugReportModel.PriorityId;
                bugReport.StatusId = 1; // Status: NEW

                bugReport.Product = db.Products.Where(a => a.Id == createBugReportModel.ProductId).Single();
                bugReport.Severity = db.Severities.Where(a => a.Id == createBugReportModel.SeverityId).Single();
                bugReport.Priority = db.Priorities.Where(a => a.Id == createBugReportModel.PriorityId).Single();
                bugReport.Status = db.Status.Where(a => a.Id == 1).Single();

                foreach (var tag in createBugReportModel.Tags)
                {
                    var _t = db.Tags.Where(a => a.Id == tag.Id).Single();

                    if (_t != null)
                    {
                        bugReport.Tags.Add(_t);
                    }
                }

                db.BugReports.Add(bugReport);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", createBugReportModel.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", createBugReportModel.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", createBugReportModel.SeverityId);
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");
            return View(createBugReportModel);
        }

        // GET: BugReports/Edit/5
        public async Task<ActionResult> Edit(int id)
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
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", bugReport.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", bugReport.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", bugReport.SeverityId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", bugReport.StatusId);
            return View(bugReport);
        }

        // POST: BugReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ProductId,AuthorId,SeverityId,PriorityId,StatusId,CreateDateTime")] BugReport bugReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bugReport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", bugReport.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", bugReport.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", bugReport.SeverityId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", bugReport.StatusId);
            return View(bugReport);
        }

        // GET: BugReports/Delete/5
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

        // POST: BugReports/Delete/5
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
