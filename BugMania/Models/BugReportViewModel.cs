using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Reports.Entities;

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
            this.Title = bugReport.Title;
            this.Description = bugReport.Description;
            this.Product = bugReport.Product;
            this.Author= bugReport.Author;
            this.Priority = bugReport.Priority;
            this.Severity = bugReport.Severity;
            this.Status = bugReport.Status;
            this.CreateDateTime = bugReport.CreateDateTime;
            this.Tags = bugReport.Tags;
            this.Comments= bugReport.Comments;
            this.Assignees = bugReport.Assignees;
        }

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

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ApplicationUser> Assignees { get; set; }
    }
}