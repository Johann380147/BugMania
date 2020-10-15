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
using BugMania.Models;
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers
{
    [RoutePrefix("Group")]
    [Authorize]
    public class CreateGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       

        // GET: Group/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugMania.Shapes.Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Group/Create
        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View("/Views/Group/ViewGroupUI.cshtml", new GroupsViewModel());
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupsViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                BugMania.Shapes.Group group = new BugMania.Shapes.Group();
                group.Name = groupViewModel.Name;
                group.ProductId = groupViewModel.ProductId;

                // Add Group first to get DB generated Id
                db.Groups.Add(group);
                await db.SaveChangesAsync();

                var member = new GroupMember();
                member.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                member.GroupId = group.Id;
                group.GroupMembers.Add(member);

                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //foreach (var member in createGroupViewModel.Members)
                //{
                //    var _t = db.Users.Where(a => a.Id == member.Id).Single();

                //    if (_t != null)
                //    {
                //        group.GroupMembers.Add(_t);
                //    }
                //}

                return RedirectToAction("View", "ViewGroup");
            }


            return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("View", "ViewGroup");
        }

        // GET: Group/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugMania.Shapes.Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", group.ProductId);
            return View(group);
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ProductId")] BugMania.Shapes.Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", group.ProductId);
            return View(group);
        }

        // GET: Group/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugMania.Shapes.Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BugMania.Shapes.Group group = await db.Groups.FindAsync(id);
            db.Groups.Remove(group);
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
