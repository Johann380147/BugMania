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
        public ActionResult EditReport(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugMania.Shapes.BugReport bugReport = bugReportEntity.GetSingleBugReport(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }

            var products = productEntity.GetAllProducts();
            var severity = severityEntity.GetAllSeverities();
            var priority = priorityEntity.GetAllPriorities();
            SelectList ProductList;
            SelectList SeverityList;
            SelectList PriorityList;

            if (User.IsInRole("Admin"))
            {
                ProductList = new SelectList(products, "Id", "Name", bugReport.ProductId);
                SeverityList = new SelectList(severity, "Id", "Name", bugReport.SeverityId);
                PriorityList = new SelectList(priority, "Id", "Name", bugReport.PriorityId);
            }
            else
            {
                ProductList = new SelectList(products, "Id", "Name", bugReport.ProductId, products.Select(t => t.Id));
                SeverityList = new SelectList(severity, "Id", "Name", bugReport.SeverityId, severity.Select(t => t.Id));
                PriorityList = new SelectList(priority, "Id", "Name", bugReport.PriorityId, priority.Select(t => t.Id));
            }

            ViewBag.ProductList = ProductList;
            ViewBag.SeverityList = SeverityList;
            ViewBag.PriorityList = PriorityList;
            ViewBag.StatusList = GetSelectListItemsForRole(bugReport.Assignees, statusEntity, bugReport.StatusId); ;
            //ViewBag.Tags = new SelectList(tagEntity.GetAllTags(), "Id", "Name");

            var viewModel = new EditBugReportViewModel(bugReport);

            return View("/Views/BugReport/EditBugReportUI.cshtml", viewModel);
        }

        // POST: BugReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReport(EditBugReportViewModel editBugReportViewModel)
        {
            if (ModelState.IsValid)
            {
                if (bugReportEntity.UpdateBugReport(editBugReportViewModel) == false) throw new Exception();
                return Redirect("/Report/Details/" + editBugReportViewModel.Id);
            }

            var products = productEntity.GetAllProducts();
            var severity = severityEntity.GetAllSeverities();
            var priority = priorityEntity.GetAllPriorities();
            SelectList ProductList;
            SelectList SeverityList;
            SelectList PriorityList;

            if (User.IsInRole("Admin"))
            {
                ProductList = new SelectList(products, "Id", "Name", editBugReportViewModel.ProductId);
                SeverityList = new SelectList(severity, "Id", "Name", editBugReportViewModel.SeverityId);
                PriorityList = new SelectList(priority, "Id", "Name", editBugReportViewModel.PriorityId);
            }
            else
            {
                ProductList = new SelectList(products, "Id", "Name", editBugReportViewModel.ProductId, products.Select(t => t.Id));
                SeverityList = new SelectList(severity, "Id", "Name", editBugReportViewModel.SeverityId, severity.Select(t => t.Id));
                PriorityList = new SelectList(priority, "Id", "Name", editBugReportViewModel.PriorityId, priority.Select(t => t.Id));
            }

            ViewBag.ProductList = ProductList;
            ViewBag.SeverityList = SeverityList;
            ViewBag.PriorityList = PriorityList;
            ViewBag.StatusList = GetSelectListItemsForRole(editBugReportViewModel.Assignees, statusEntity, editBugReportViewModel.StatusId); ;


            return View("/Views/BugReport/EditBugReportUI.cshtml", editBugReportViewModel);
        }

        private IEnumerable<SelectListItem> GetSelectListItemsForRole(ICollection<ApplicationUser> assignees, StatusEntity statusEntity, int selectedItem)
        {
            var list = statusEntity.GetAllStatus();
            var selectItem = new List<SelectListItem>();

            foreach (var item in list)
            {
                selectItem.Add(new SelectListItem
                {
                    Value = Convert.ToString(item.Id),
                    Text = item.Name,
                    Selected = (item.Id == selectedItem)
                });
            }
            if (User.IsInRole("Admin"))
            {
                selectItem.First(t => t.Text == "NEW").Disabled = false;
                selectItem.First(t => t.Text == "ASSIGNED").Disabled = false;
                selectItem.First(t => t.Text == "UNVERIFIED_FIXED").Disabled = false;
                selectItem.First(t => t.Text == "VERIFIED_FIXED").Disabled = false;
                selectItem.First(t => t.Text == "INVALID").Disabled = false;
                selectItem.First(t => t.Text == "DUPLICATE").Disabled = false;
            }
            else if (User.IsInRole("Triager"))
            {
                selectItem.First(t => t.Text == "NEW").Disabled = false;
                selectItem.First(t => t.Text == "ASSIGNED").Disabled = false;
                selectItem.First(t => t.Text == "UNVERIFIED_FIXED").Disabled = true;
                selectItem.First(t => t.Text == "VERIFIED_FIXED").Disabled = true;
                selectItem.First(t => t.Text == "INVALID").Disabled = false;
                selectItem.First(t => t.Text == "DUPLICATE").Disabled = false;
            }
            else if (User.IsInRole("Developer") &&
                assignees.FirstOrDefault(i => i.Id == User.Identity.GetUserId()) != null)
            {
                selectItem.First(t => t.Text == "NEW").Disabled = true;
                selectItem.First(t => t.Text == "ASSIGNED").Disabled = true;
                selectItem.First(t => t.Text == "UNVERIFIED_FIXED").Disabled = false;
                selectItem.First(t => t.Text == "VERIFIED_FIXED").Disabled = true;
                selectItem.First(t => t.Text == "INVALID").Disabled = true;
                selectItem.First(t => t.Text == "DUPLICATE").Disabled = true;
            }
            else if (User.IsInRole("Reviewer"))
            {
                selectItem.First(t => t.Text == "NEW").Disabled = true;
                selectItem.First(t => t.Text == "ASSIGNED").Disabled = true;
                selectItem.First(t => t.Text == "UNVERIFIED_FIXED").Disabled = false;
                selectItem.First(t => t.Text == "VERIFIED_FIXED").Disabled = false;
                selectItem.First(t => t.Text == "INVALID").Disabled = true;
                selectItem.First(t => t.Text == "DUPLICATE").Disabled = true;
            }
            else
            {
                foreach (var item in selectItem)
                {
                    item.Disabled = true;
                }
            }

            return selectItem;
        }
    }
}
