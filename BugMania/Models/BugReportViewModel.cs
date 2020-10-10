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
        public CreateBugReportViewModel()
        {
            Tags = new HashSet<Tag>();
        }

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
}