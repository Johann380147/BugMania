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
using System.Data.Entity.Infrastructure;

namespace BugMania.Entities
{
    public class BugReportEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Find - gets local, SingleOrDefault - force get from DB
        // .Include loads associated data from parent table into child object
        public IQueryable<BugReport> GetSetOfReports(int skipCount, int takeCount)
        {
            var model = db.BugReports
                .Include(b => b.Priority)
                .Include(b => b.Product)
                .Include(b => b.Severity)
                .Include(b => b.Status)
                .Include(b => b.Tags)
                .OrderByDescending(t => t.LastEditDateTime)
                .ThenByDescending(t => t.CreateDateTime)
                .Skip(skipCount)
                .Take(takeCount);
            return model;
        }

        public IQueryable<BugReport> GetFilteredSetOfReports(int skipCount, int takeCount, string filter = "")
        {
            var model = db.BugReports.Where(
               b => b.Title.Contains(filter) ||
               b.Description.Contains(filter) ||
               b.Product.Name.Contains(filter) ||
               b.Assignees.Any(a => a.Email.Contains(filter)) ||
               b.Tags.Any(t => t.Name.Contains(filter)))
                .Include(b => b.Priority)
                .Include(b => b.Product)
                .Include(b => b.Severity)
                .Include(b => b.Status)
                .Include(b => b.Tags)
                .OrderByDescending(t => t.LastEditDateTime)
                .ThenByDescending(t => t.CreateDateTime)
                .Skip(skipCount)
                .Take(takeCount);
            return model;
        }



        public BugReport GetSingleBugReport(int id)
        {
            BugReport bugReport = db.BugReports
                                    .Include(b => b.Product)
                                    .Include(b => b.Author)
                                    .Include(b => b.Severity)
                                    .Include(b => b.Priority)
                                    .Include(b => b.Status)
                                    .Include(b => b.Tags)
                                    .Include(b => b.Comments.Select(c => c.Commenter))
                                    .Include(b => b.Assignees)
                                    .Include(b => b.Subscribers)
                                    .Include(b => b.EditLog)
                                    .First(b => b.Id == id);

            return bugReport;
        }

        public bool AddBugReport(CreateBugReportViewModel createBugReportViewModel)
        {
            BugReport bugReport = new BugReport();
            var currDate = DateTime.UtcNow;

            bugReport.AuthorId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            bugReport.CreateDateTime = currDate;

            bugReport.Title = createBugReportViewModel.Title;
            bugReport.Description = createBugReportViewModel.Description;
            bugReport.ProductId = createBugReportViewModel.ProductId;
            bugReport.SeverityId = createBugReportViewModel.SeverityId;
            bugReport.PriorityId = createBugReportViewModel.PriorityId;
            bugReport.StatusId = 1; // Status: NEW
            
            bugReport.EditLog.Add(new Log
            {
                BugReportId = bugReport.Id,
                EditorId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                EditDateTime = currDate,
                OperationId = 1,
                Status = "NEW"
            });

            foreach (var tag in createBugReportViewModel.Tags)
            {
                var _t = db.Tags
                    .Where(a => a.Id == tag.Id)
                    .Single();

                if (_t != null)
                {
                    bugReport.Tags.Add(_t);
                }
            }

            db.BugReports.Add(bugReport);

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

        public bool UpdateBugReport(EditBugReportViewModel editBugReportViewModel)
        {

            ApplicationUserEntity userEntity = new ApplicationUserEntity();
            

            BugReport bugReport = GetSingleBugReport(editBugReportViewModel.Id);

            bugReport.Title = editBugReportViewModel.Title;
            bugReport.Description = editBugReportViewModel.Description;

            bugReport.ProductId = editBugReportViewModel.ProductId;
            bugReport.SeverityId = editBugReportViewModel.SeverityId;
            bugReport.PriorityId = editBugReportViewModel.PriorityId;

            string statusName;

            if (bugReport.StatusId == editBugReportViewModel.StatusId)
            {
                statusName = "UNCHANGED";
            }
            else
            {
                statusName = db.Status
                    .FirstOrDefault(i => i.Id == bugReport.StatusId)
                    .Name;
                bugReport.StatusId = editBugReportViewModel.StatusId;
            }
            

            var currDate = DateTime.UtcNow;
            bugReport.LastEditDateTime = currDate;

            if (bugReport.EditLog == null)
            {
                bugReport.EditLog = new HashSet<Log>();
            }
            bugReport.EditLog.Add(new Log
            {
                BugReportId = bugReport.Id,
                EditorId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                EditDateTime = currDate,
                OperationId = 2,
                Status = statusName
            });

            bugReport.Assignees.Clear();
            foreach (var em in editBugReportViewModel.Email)
            {
                var user = userEntity.GetUserByEmail(em, db);
                if (user != null)
                {
                    bugReport.Assignees.Add(user);
                }
            }

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public bool AddSubscriber(int bugReportId, string userId)
        {
            var bugReport = this.GetSingleBugReport(bugReportId);
            var user = db.Users.FirstOrDefault(i => i.Id == userId);


            if (bugReport != null && user != null)
            {
                bugReport.Subscribers.Add(user);

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveSubscriber(int bugReportId, string subscriberId)
        {
            var bugReport = GetSingleBugReport(bugReportId);
            
            var user = db.Users.FirstOrDefault(i => i.Id == subscriberId);

            if (bugReport != null && user != null)
            {
                foreach (var subscriber in bugReport.Subscribers.Where(i => i.Id.Contains(user.Id)).ToList())
                {
                    bugReport.Subscribers.Remove(subscriber);
                }

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool DeleteBugReport(int id)
        {
            try
            {
                BugReport bugReport = this.GetSingleBugReport(id);
                db.BugReports.Remove(bugReport);
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