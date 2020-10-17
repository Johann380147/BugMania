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

namespace BugMania.Entities
{
    public class CommentEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Find - gets local, SingleOrDefault - force get from DB
        // .Include loads associated data from parent table into child object
        public List<Comment> GetAllComments(int id)
        {
            IQueryable<Comment> comments;

            comments = db.Comments
                .Include(b => b.Commenter)
                .Where(br => br.BugReportId == id);

            var result = comments.ToList();
            if (result != null)
            {
                var sorted = result
                    .OrderByDescending(t => t.CommentDateTime)
                    .ToList();

                return sorted;
            }
            else
            {
                return null;
            }
        }

        public bool AddComment(CreateCommentViewModel createCommentViewModel)
        {
            var comment = new Comment();
            comment.BugReportId = createCommentViewModel.BugReportId;
            comment.Content = createCommentViewModel.Content;
            comment.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            comment.CommentDateTime = DateTime.UtcNow;

            try
            {
                comment.BugReport = db.BugReports
                    .Where(a => a.Id == comment.BugReportId)
                    .Single();

                db.Comments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateComment(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteComment(int id)
        {
            var comment = db.Comments.Find(id);
            db.Comments.Remove(comment);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}