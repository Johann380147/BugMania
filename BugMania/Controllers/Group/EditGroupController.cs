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

namespace BugMania.Controllers.Group
{
    public class EditGroupController : Controller
    {
        private GroupEntity groupEntity = new GroupEntity();

        // GET: Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (group == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", group.ProductId);
            return View();
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProductId")] BugMania.Shapes.Group group)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", group.ProductId);
            return View(group);
        }
    }
}