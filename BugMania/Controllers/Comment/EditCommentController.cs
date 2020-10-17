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

namespace BugMania.Controllers.Comment
{
    public class EditCommentController : Controller
    {
        CommentEntity commentEntity = new CommentEntity();

        // GET: Comment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BugReportId,Content,CommentDateTime,UserId")] BugMania.Shapes.Comment comment)
        {
            if (ModelState.IsValid)
            {
                commentEntity.UpdateComment(comment);
                return RedirectToAction("ViewAllReports", "ViewAllBugReport");
            }
            return View(comment);
        }
    }
}