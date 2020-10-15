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
        public async Task<List<Comment>> GetAllComments(int id)
        {
            IQueryable<Comment> comments;

            comments = db.Comments
                .Include(b => b.Commenter)
                .Where(br => br.BugReportId == id);

            var result = await comments.ToListAsync();
            if (result != null)
            {
                var sorted = result.OrderByDescending(t => t.CommentDateTime).ToList();
                return sorted;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddComment(CreateCommentViewModel createCommentViewModel)
        {
            var comment = new Comment();
            comment.BugReportId = createCommentViewModel.BugReportId;
            comment.Content = createCommentViewModel.Content;
            comment.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            comment.CommentDateTime = DateTime.UtcNow;

            var _c = db.BugReports.Where(a => a.Id == comment.BugReportId).Single();

            if (_c != null)
            {
                comment.BugReport = _c;
            }


            db.Comments.Add(comment);
            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}