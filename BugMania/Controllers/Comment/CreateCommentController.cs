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
using BugMania.Entities;
using BugMania.Hubs;
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers.Comment
{
    public class CreateCommentController : Controller
    {
        CommentEntity commentEntity = new CommentEntity();
        BugReportEntity bugReportEntity = new BugReportEntity();

        // GET: Comment/Create
        public ActionResult Create()
        {
            return RedirectToAction("ViewAllReports", "ViewAllBugReport");
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCommentViewModel createCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                if (createCommentViewModel.Content.Trim() == "")
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }

                commentEntity.AddComment(createCommentViewModel);

                var bugReport = bugReportEntity.GetSingleBugReport(createCommentViewModel.BugReportId);
                if (bugReport.Subscribers != null &&
                    bugReport.Subscribers.Count > 0)
                {
                    var usersId = bugReport.Subscribers.Select(i => i.UserName).ToList();
                    UserHub.NotifyBugReportChangeToSubscribers(usersId, createCommentViewModel.BugReportId, createCommentViewModel.Content, "Comment");
                }

                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
