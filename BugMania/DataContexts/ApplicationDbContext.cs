using BugMania.Shapes;

namespace BugMania.DataContexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BugMania.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using BugMania.Shapes;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Severity> Severities { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<BugReport> BugReports { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }


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
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<BugReport>()
                .HasRequired(e => e.Author)
                .WithMany(e => e.Composer)
                .HasForeignKey<string>(s => s.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BugReport>()
                .HasMany(e => e.Subscribers)
                .WithMany(e => e.SubscriptionList)
                .Map(m => m.ToTable("BugReportSubscribers").MapLeftKey("BugReportId").MapRightKey("UserId"));

            modelBuilder.Entity<BugReport>()
                .HasMany(e => e.Assignees)
                .WithMany(e => e.AssignedReports)
                .Map(m => m.ToTable("BugReportAssignees").MapLeftKey("BugReportId").MapRightKey("UserId"));

            modelBuilder.Entity<BugReport>()
                .HasRequired(e => e.Product)
                .WithMany(e => e.BugReports)
                .HasForeignKey<int>(s => s.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BugReport>()
                .HasRequired(e => e.Severity)
                .WithMany(e => e.BugReports)
                .HasForeignKey<int>(s => s.SeverityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BugReport>()
                .HasRequired(e => e.Priority)
                .WithMany(e => e.BugReports)
                .HasForeignKey<int>(s => s.PriorityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BugReport>()
                .HasRequired(e => e.Status)
                .WithMany(e => e.BugReports)
                .HasForeignKey<int>(s => s.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BugReport>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.BugReports)
                .Map(m => m.ToTable("BugReportTags").MapLeftKey("BugReportId").MapRightKey("TagId"));

            modelBuilder.Entity<Comment>()
                .HasRequired(e => e.BugReport)
                .WithMany(e => e.Comments)
                .HasForeignKey<int>(s => s.BugReportId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
                .HasRequired(e => e.Commenter)
                .WithMany(e => e.CommentsMade)
                .HasForeignKey<string>(s => s.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Group>()
                .HasRequired(e => e.Product)
                .WithMany(e => e.Groups)
                .HasForeignKey<int>(s => s.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.GroupMembers)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("GroupMembers").MapLeftKey("GroupId").MapRightKey("MemberId"));

            modelBuilder.Entity<Log>()
                .HasRequired(e => e.Editor)
                .WithMany(e => e.EditedReports)
                .HasForeignKey<string>(s => s.EditorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .HasRequired(e => e.BugReport)
                .WithMany(e => e.EditLog)
                .HasForeignKey<int>(s => s.BugReportId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .HasRequired(e => e.Operation)
                .WithMany(e => e.Logs)
                .HasForeignKey<int>(s => s.OperationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey<int?>(s => s.RoleId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}
