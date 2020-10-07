using Reports.Entities;

namespace BugMania.DataContexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BugMania.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<BugReport> BugReports { get; set; }
        public virtual DbSet<BugReportAssignee> BugReportAssignees { get; set; }
        public virtual DbSet<BugReportSubscriber> BugReportSubscribers { get; set; }
        public virtual DbSet<BugReportComment> BugReportComments { get; set; }
        public virtual DbSet<BugReportTag> BugReportTags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Severity> Severities { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BugReport>()
                .HasMany(e => e.Comments)
                .WithMany(e => e.BugReports)
                .Map(m => m.ToTable("BugReportComments").MapLeftKey("BugReportId").MapRightKey("CommentId"));

            modelBuilder.Entity<BugReport>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.BugReports)
                .Map(m => m.ToTable("BugReportTags").MapLeftKey("BugReportId").MapRightKey("TagId"));

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BugReports)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.BugReports)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
