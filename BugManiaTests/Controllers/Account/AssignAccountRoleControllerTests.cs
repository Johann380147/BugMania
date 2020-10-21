using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugMania.Controllers.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BugMania.Models;

namespace BugMania.Controllers.Account.Tests
{
    [TestClass()]
    public class AssignAccountRoleControllerTests
    {
        [TestMethod()]
        public void Get_AssignAccountRolePage_Test()
        {
            AssignAccountRoleController controller = new AssignAccountRoleController();
            ViewResult result = controller.Assign() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}