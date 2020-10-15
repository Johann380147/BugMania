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
    public class EditBugReportController : Controller
    {
        private BugReportEntity bugReportEntity = new BugReportEntity();
        private PriorityEntity priorityEntity = new PriorityEntity();
        private SeverityEntity severityEntity = new SeverityEntity();
        private ProductEntity productEntity = new ProductEntity();
        private StatusEntity statusEntity = new StatusEntity();
        private TagEntity tagEntity = new TagEntity();

        // GET: BugReport/Edit/5
        [Route("Edit/{id}")]
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

            ViewBag.PriorityId = new SelectList(await priorityEntity.GetAllPriorities(), "Id", "Name", bugReport.PriorityId);
            ViewBag.SeverityId = new SelectList(await severityEntity.GetAllSeverities(), "Id", "Name", bugReport.SeverityId);
            ViewBag.ProductId = new SelectList(await productEntity.GetAllProducts(), "Id", "Name", bugReport.ProductId);
            ViewBag.StatusId = new SelectList(await statusEntity.GetAllStatus(), "Id", "Name", bugReport.StatusId);
            ViewBag.Tags = new SelectList(await tagEntity.GetAllTags(), "Id", "Name");

            var viewModel = new EditBugReportViewModel(bugReport);

            return View("/Views/BugReport/EditBugReportUI.cshtml", viewModel);
        }

        // POST: BugReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBugReportViewModel editBugReportViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await bugReportEntity.UpdateBugReport(editBugReportViewModel) == false) throw new Exception();
                return Redirect("/Report/Details/" + editBugReportViewModel.Id);
            }

            ViewBag.PriorityId = new SelectList(await priorityEntity.GetAllPriorities(), "Id", "Name", editBugReportViewModel.PriorityId);
            ViewBag.SeverityId = new SelectList(await severityEntity.GetAllSeverities(), "Id", "Name", editBugReportViewModel.SeverityId);
            ViewBag.ProductId = new SelectList(await productEntity.GetAllProducts(), "Id", "Name", editBugReportViewModel.ProductId);
            ViewBag.StatusId = new SelectList(await statusEntity.GetAllStatus(), "Id", "Name", editBugReportViewModel.StatusId);
            
            return View("/Views/BugReport/EditBugReportUI.cshtml", editBugReportViewModel);
        }
    }
}
