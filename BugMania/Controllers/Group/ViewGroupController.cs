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
using Microsoft.AspNet.Identity;
using BugMania.Entities;

namespace BugMania.Controllers.Group
{
    [RoutePrefix("Group")]
    public class ViewGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProductEntity productEntity = new ProductEntity();

        // GET: Group
        [Route]
        [Route("View")]
        public async Task<ActionResult> View()
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == id)
                .Include(g => g.MemberOf.Select(m => m.Group)) // Includes MemberOf Collection followed by MemberOf.Group
                .Single();
            var groupsViewModel = new GroupsViewModel(user);

            ViewBag.ProductId = new SelectList(await productEntity.GetAllProducts(), "Id", "Name");
            return View("/Views/Group/ViewGroupUI.cshtml", groupsViewModel);
        }
    }
}