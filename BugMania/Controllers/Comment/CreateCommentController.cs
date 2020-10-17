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
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers
{
    public class CreateCommentController : Controller
    {
        CommentEntity commentEntity = new CommentEntity();

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

                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
