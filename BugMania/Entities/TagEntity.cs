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

namespace BugMania.Entities
{
    public class TagEntity
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Find - gets local, SingleOrDefault - force get from DB
        // .Include loads associated data from parent table into child object
        public List<Tag> GetAllTags()
        {
            IQueryable<Tag> tag;

            tag = db.Tags;

            var result = tag.ToList();
            return result;
        }

        public bool AddTag(Tag tag)
        {
            db.Tags.Add(tag);

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