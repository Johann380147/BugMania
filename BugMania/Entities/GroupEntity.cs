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
    public class GroupEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool AddGroup(Group group)
        {
            // Add Group first to get DB generated Id
            var _group = group;
            db.Groups.Add(_group);
            db.SaveChanges();
            
            

            var member = new GroupMember();
            member.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            member.GroupId = _group.Id;
            group.GroupMembers.Add(member);

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