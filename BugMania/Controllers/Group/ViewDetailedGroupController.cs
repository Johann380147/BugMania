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
using BugMania.Entities;
using BugMania.Models;
using Microsoft.AspNet.Identity;

namespace BugMania.Controllers.Group
{
    public class ViewDetailedGroupController : Controller
    {
        private GroupEntity groupEntity = new GroupEntity();

        // GET: Group/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BugMania.Shapes.Group group = await db.Groups.FindAsync(id);
            //if (group == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }
    }
}