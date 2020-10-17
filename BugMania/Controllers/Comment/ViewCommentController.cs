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
    public class ViewCommentController : Controller
    {
        CommentEntity commentEntity = new CommentEntity();
        // GET: Comment
        public ActionResult Index(int id)
        {
            var comments = commentEntity.GetAllComments(id);
            return View(comments);
        }
    }
}