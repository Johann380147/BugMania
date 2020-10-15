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
        public async Task<List<BugReport>> GetAllBugReports()
        {
            IQueryable<BugReport> bugReports;

            bugReports = db.BugReports
                .Include(b => b.Priority)
                .Include(b => b.Product)
                .Include(b => b.Severity)
                .Include(b => b.Status)
                .Include(b => b.Tags);

            var result = await bugReports.ToListAsync();
            if (result != null)
            {
                var sorted = result.OrderByDescending(t => t.CreateDateTime).ToList();
                return sorted;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<BugReport>> GetFilteredBugReports(string filter = "")
        {
            IQueryable<BugReport> bugReports;

            bugReports = db.BugReports.Where(
               b => b.Title.Contains(filter) ||
               b.Description.Contains(filter) ||
               b.Product.Name.Contains(filter) ||
               b.Assignees.Any(a => a.Email.Contains(filter)) ||
               b.Tags.Any(t => t.Name.Contains(filter)))
               .Include(b => b.Priority)
               .Include(b => b.Product)
               .Include(b => b.Severity)
               .Include(b => b.Status)
               .Include(b => b.Tags);

            var result = await bugReports.ToListAsync();
            if (result != null)
            {
                var sorted = result.OrderByDescending(t => t.CreateDateTime).ToList();
                return sorted;
            }
            else
            {
                return null;
            }
        }

        public async Task<BugReport> GetSingleBugReport(int id)
        {
            BugReport bugReport = await db.BugReports
                                    .Include(b => b.Product)
                                    .Include(b => b.Author)
                                    .Include(b => b.Severity)
                                    .Include(b => b.Priority)
                                    .Include(b => b.Status)
                                    .Include(b => b.Tags)
                                    .Include(b => b.Comments.Select(c => c.Commenter))
                                    .Include(b => b.Assignees)
                                    .Include(b => b.EditLog)
                                    .FirstAsync(b => b.Id == id);

            return bugReport;
        }

        public async Task<bool> AddBugReport(CreateBugReportViewModel createBugReportViewModel)
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
                OperationId = 1
            });

            foreach (var tag in createBugReportViewModel.Tags)
            {
                var _t = db.Tags.Where(a => a.Id == tag.Id).Single();

                if (_t != null)
                {
                    bugReport.Tags.Add(_t);
                }
            }

            db.BugReports.Add(bugReport);

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

        public async Task<bool> UpdateBugReport(EditBugReportViewModel editBugReportViewModel)
        {

            ApplicationUserEntity userEntity = new ApplicationUserEntity();
            

            BugReport bugReport = await GetSingleBugReport(editBugReportViewModel.Id);

            bugReport.Title = editBugReportViewModel.Title;
            bugReport.Description = editBugReportViewModel.Description;

            bugReport.ProductId = editBugReportViewModel.ProductId;
            bugReport.SeverityId = editBugReportViewModel.SeverityId;
            bugReport.PriorityId = editBugReportViewModel.PriorityId;
            bugReport.StatusId = editBugReportViewModel.StatusId;
            

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
                OperationId = 2
            });

            bugReport.Assignees.Clear();
            foreach (var em in editBugReportViewModel.Email)
            {
                var user = await userEntity.GetUserByEmail(em, db);
                if (user != null)
                {
                    bugReport.Assignees.Add(user);
                }
            }

            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }
        
        public async Task<bool> DeleteBugReport(int id)
        {
            try
            {
                BugReport bugReport = await this.GetSingleBugReport(id);
                db.BugReports.Remove(bugReport);
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