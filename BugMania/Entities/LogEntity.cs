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
    public class LogEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ICollection<Log> GetLogs(int days)
        {
            var currDate = DateTime.UtcNow.AddDays(days * -1);
            var logs = db.Logs
                .Include(o => o.Operation)
                .Include(e => e.Editor)
                .Include(r => r.Editor.Role)
                .Include(b => b.BugReport)
                .Include(s => s.BugReport.Status)
                .Where(t => t.EditDateTime >= currDate)
                .ToList();

            return logs;
        }

        public ICollection<Log> GetAllLogs()
        {
            var currDate = DateTime.UtcNow;
            var logs = db.Logs
                .ToList();

            return logs;
        }
    }
}