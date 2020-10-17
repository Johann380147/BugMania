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
using BugMania.Entities;
using BugMania.Models;
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers
{
    [RoutePrefix("Group")]
    [Authorize]
    public class CreateGroupController : Controller
    {
        private GroupEntity groupEntity = new GroupEntity();
        private ProductEntity productEntity = new ProductEntity();

        // GET: Group/Create
        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(productEntity.GetAllProducts(), "Id", "Name");
            return View("/Views/Group/ViewGroupUI.cshtml", new GroupsViewModel());
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupsViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                BugMania.Shapes.Group group = new BugMania.Shapes.Group();
                group.Name = groupViewModel.Name;
                group.ProductId = groupViewModel.ProductId;

                groupEntity.AddGroup(group);

                return RedirectToAction("View", "ViewGroup");
            }

            return RedirectToAction("View", "ViewGroup");
        }
    }
}
