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
    public class RoleEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public  IEnumerable<Role> GetAllRoles()
        {
            var result = db.Roles;
            return result;
        }

        public int GetRoleId (string roleName)
        {
            var result = db.Roles
                .Where(n => n.Name == roleName)
                .FirstOrDefault();

            if (result != null)
            {
                return result.Id;
            }
            else
            {
                return -1;
            }
        }

        public string GetRoleName(int id)
        {
            var result = db.Roles
                .Where(i => i.Id == id)
                .FirstOrDefault();
            if (result != null)
            {
                return result.Name;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}