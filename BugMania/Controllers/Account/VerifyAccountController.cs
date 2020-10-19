using BugMania.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugMania.Controllers.Account
{
    public class VerifyAccountController : Controller
    {
        private ApplicationUserEntity userEntity = new ApplicationUserEntity();

        [HttpPost]
        public bool Verify(string email)
        {
            if (userEntity.VerifyUserRole(email, "Developer"))
                return true;
            else
                return false;
        }
    }
}