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
        private ApplicationUserEntity userEntity = new ApplicationUserEntity();
        private ProductEntity productEntity = new ProductEntity();

        // GET: Group
        [Route]
        [Route("View")]
        public ActionResult View()
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = userEntity.GetUserWithGroup(id);
            var groupsViewModel = new GroupsViewModel(user);

            ViewBag.ProductId = new SelectList(productEntity.GetAllProducts(), "Id", "Name");
            return View("/Views/Group/ViewGroupUI.cshtml", groupsViewModel);
        }
    }
}