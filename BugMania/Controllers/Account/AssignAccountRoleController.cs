﻿using System;
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

namespace BugMania.Controllers.Account
{
    [RoutePrefix("Account/Role")]
    [Authorize(Roles = "Admin")]
    public class AssignAccountRoleController : Controller
    {
        ApplicationUserEntity userEntity = new ApplicationUserEntity();
        RoleEntity roleEntity = new RoleEntity();

        [Route("Assign")]
        public ActionResult Assign()
        {
            List<AssignAccountRoleViewModel> accounts = new List<AssignAccountRoleViewModel>();

            foreach (var user in userEntity.GetAllUsers())
            {
                accounts.Add(new AssignAccountRoleViewModel(user));
            }
            
            ViewBag.RoleId = roleEntity.GetAllRoles();
            return View("/Views/Account/AssignAccountRoleUI.cshtml", accounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(IList<AssignAccountRoleViewModel> assignAccountRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (userEntity.UpdateUsersRole(assignAccountRoleViewModel) == false)
                {
                    throw new Exception("Could not update user roles.");
                }
            }
            return Redirect("/Account/Role/Assign");
        }
    }
}