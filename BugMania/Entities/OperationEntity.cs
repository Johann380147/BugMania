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
    public class OperationEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetTypeOfOperation(int id)
        {
            return db.Operations.FirstOrDefault(i => i.Id == id).Type;
        }
    }
}