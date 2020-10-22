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
using Microsoft.AspNet.Identity.Owin;
using BugMania.Models;
using BugMania.Helpers;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

namespace BugMania.Entities
{
    public class ApplicationUserEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser user;

            user = db.Users
                .Include(b => b.Role)
                .Include(b => b.Groups)
                .FirstOrDefault(i => i.Id == id);

            return user;
        }

        public ApplicationUser GetUserByIdNoTracking(string id)
        {
            ApplicationUser user;

            user = db.Users
                .AsNoTracking()
                .FirstOrDefault(i => i.Id == id);

            return user;
        }

        public ApplicationUser GetUserByEmail(string email, ApplicationDbContext context)
        {
            ApplicationUser user;
            
            user = context.Users
                .FirstOrDefault(i => i.Email == email);

            return user;
        }

        public Role GetUserRole (string username)
        {
            ApplicationUser user;

            user = db.Users
                .Include(b => b.Role)
                .FirstOrDefault(i => i.UserName == username);

            if (user != null)
            {
                return user.Role;
            }
            else
            {
                return null;
            }
        }

        public ApplicationUser GetUserWithGroup (string id)
        {
                var user = db.Users
                    .Where(u => u.Id == id)
                    .Include(g => g.Groups)
                    .SingleOrDefault();
                return user;   
        }

        public bool VerifyUserRole(string email, string role)
        {
            var user = db.Users
                .Include(b => b.Role)
                .FirstOrDefault(i => i.Email == email);
            
            if (user != null)
            {
                Role userRole = user.Role;
                if (userRole.Name.ToLower() == role.ToLower())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool Exists (string email)
        {
            ApplicationUser user;

            user = db.Users
                .FirstOrDefault(i => i.Email == email);

            if (user != null)
                return true;
            else
                return false;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return db.Users
                .Include(a => a.Role)
                .ToList();
        }

        public bool UpdateUsersRole(IList<AssignAccountRoleViewModel> usersViewModel)
        {
            List<ApplicationUser> lst = new List<ApplicationUser>();
            var context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

            foreach (var userVM in usersViewModel)
            {
                var user = context.Users.Include(r => r.Role)
                    .FirstOrDefault(i => i.Id == userVM.Id);
                if (user != null)
                {
                    user.RoleId = userVM.RoleId;
                    lst.Add(user);
                }
            }
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public void DetachContext(ApplicationUser user)
        {
            db.Entry(user).State = EntityState.Detached;
        }
    }
}