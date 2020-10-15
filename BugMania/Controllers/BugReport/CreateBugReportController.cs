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
    [Authorize]
    public class CreateBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();
        private PriorityEntity priorityEntity = new PriorityEntity();
        private SeverityEntity severityEntity = new SeverityEntity();
        private ProductEntity productEntity = new ProductEntity();
        private StatusEntity statusEntity = new StatusEntity();
        private TagEntity tagEntity = new TagEntity();


        // GET: Report/Create
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            CreateBugReportViewModel model = new CreateBugReportViewModel();

            ViewBag.PriorityId = new SelectList(await priorityEntity.GetAllPriorities(), "Id", "Name");
            ViewBag.SeverityId = new SelectList(await severityEntity.GetAllSeverities(), "Id", "Name");
            ViewBag.ProductId = new SelectList(await productEntity.GetAllProducts(), "Id", "Name");
            ViewBag.Tags = new SelectList(await tagEntity.GetAllTags(), "Id", "Name");
            return View("/Views/BugReport/CreateBugReportUI.cshtml", model);
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

                return RedirectToAction("View", "ViewAllBugReport");
            }

            ViewBag.PriorityId = new SelectList(await priorityEntity.GetAllPriorities(), "Id", "Name", createBugReportViewModel.PriorityId);
            ViewBag.SeverityId = new SelectList(await severityEntity.GetAllSeverities(), "Id", "Name", createBugReportViewModel.ProductId);
            ViewBag.ProductId = new SelectList(await productEntity.GetAllProducts(), "Id", "Name", createBugReportViewModel.SeverityId);
            ViewBag.Tags = new SelectList(await tagEntity.GetAllTags(), "Id", "Name");

            return View("/Views/BugReport/CreateBugReportUI.cshtml",createBugReportViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> GetAllTags(string value)
        {
            var tags = new SelectList(await tagEntity.GetAllTags(), "Id", "Name").ToList();
            
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}
