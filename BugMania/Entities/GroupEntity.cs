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
            foreach (var member in group.GroupMembers)
            {
                db.Users.Attach(member);
            }
            
            db.Groups.Add(group);
            db.SaveChanges();
            
            
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