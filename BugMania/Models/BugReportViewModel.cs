using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BugMania.Shapes;

namespace BugMania.Models
{
    public class CreateBugReportViewModel
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "Severity")]
        public int SeverityId { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }

    public class DetailsBugReportViewModel
    {

        public DetailsBugReportViewModel(BugReport bugReport)
        {
            this.Id = bugReport.Id;
            this.Title = bugReport.Title;
            this.Description = bugReport.Description;
            this.Product = bugReport.Product;
            this.Author = bugReport.Author;
            this.Priority = bugReport.Priority;
            this.Severity = bugReport.Severity;
            this.Status = bugReport.Status;
            this.CreateDateTime = bugReport.CreateDateTime;
            this.LastEditDateTime = bugReport.LastEditDateTime;
            this.Tags = bugReport.Tags;
            this.Comments = bugReport.Comments;
            this.Assignees = bugReport.Assignees;
            this.Subscribers = bugReport.Subscribers;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Product Product { get; set; }

        public ApplicationUser Author { get; set; }

        public Priority Priority { get; set; }

        public Severity Severity { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime? LastEditDateTime { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ApplicationUser> Assignees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ApplicationUser> Subscribers { get; set; }


        public ICollection<Comment> getCommentsDescending
        {
            get { return Comments.OrderByDescending(c => c.CommentDateTime).ToList(); }
        }
    }

    public class CreateCommentViewModel
    {
        [Required]
        public int BugReportId { get; set; }

        [Required]
        public string Content { get; set; }

    }

    public class GroupsViewModel
    {
        public GroupsViewModel() { }
        public GroupsViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
            this.MemberOf = user.Groups;
        }

        public string UserName { get; set; }

        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public ICollection<Group> MemberOf { get; set; }
    }

    public class EditBugReportViewModel
    {
        public EditBugReportViewModel()
        {
            this.Email = new List<string>();
        }
        public EditBugReportViewModel(BugReport bugReport)
        {
            this.Id = bugReport.Id;
            this.Title = bugReport.Title;
            this.Description = bugReport.Description;
            this.ProductId = bugReport.ProductId;
            this.PriorityId = bugReport.PriorityId;
            this.SeverityId = bugReport.SeverityId;
            this.StatusId = bugReport.StatusId;
            this.Assignees = bugReport.Assignees;
            this.EditLog = bugReport.EditLog;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "Severity")]
        public int SeverityId { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ApplicationUser> Assignees { get; set; }

        public ICollection<string> Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Log> EditLog { get; set; }
    }

    public class AssignAccountRoleViewModel
    {
        public AssignAccountRoleViewModel() { }
        public AssignAccountRoleViewModel(ApplicationUser user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.RoleId = (int)user.RoleId;
            this.Role = user.Role;
        }

        [Required]
        public string Id { get; set; }

        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }

    public class LogViewModel
    {
        public int numberWeeklyNewReports { get; set; }
        public int numberWeeklyResolvedReports { get; set; }
        public int numberMonthlyNewReports { get; set; }
        public int numberMonthlyResolvedReports { get; set; }

        public IEnumerable<LogCount> TopThreeReported { get; set; }
        public IEnumerable<LogCount> TopThreeDeveloper { get; set; }
        public IEnumerable<LogCount> TopThreeReporter { get; set; }
    }

    public class LogCount
    {
        public Log Log { get; set; }
        public int Count { get; set; }
    }
}