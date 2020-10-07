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

namespace BugMania.Controllers
{
    public class BugReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BugReports
        public async Task<ActionResult> Index()
        {
            var bugReports = db.BugReports.Include(b => b.Priority).Include(b => b.Product).Include(b => b.Severity).Include(b => b.Status);
            return View(await bugReports.ToListAsync());
        }

        // GET: BugReports/Details/5
        public async Task<ActionResult> Details(string id)
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

        // GET: BugReports/Create
        public ActionResult Create()
        {
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            return View();
        }

        // POST: BugReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ProductId,AuthorId,SeverityId,PriorityId,StatusId,CreateDateTime")] BugReport bugReport)
        {
            if (ModelState.IsValid)
            {
                db.BugReports.Add(bugReport);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", bugReport.PriorityId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", bugReport.ProductId);
            ViewBag.SeverityId = new SelectList(db.Severities, "Id", "Name", bugReport.SeverityId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", bugReport.StatusId);
            return View(bugReport);
        }

        // GET: BugReports/Edit/5
        public async Task<ActionResult> Edit(string id)
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
        public async Task<ActionResult> Delete(string id)
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
        public async Task<ActionResult> DeleteConfirmed(string id)
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
