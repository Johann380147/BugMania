namespace Reports.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BugReport")]
    public partial class BugReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BugReport()
        {
            BugReportAssignees = new HashSet<BugReportAssignee>();
            BugReportSubscribers = new HashSet<BugReportSubscriber>();
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name ="Product")]
        public string ProductId { get; set; }

        
        [StringLength(128)]
        public string AuthorId { get; set; }

        [StringLength(128)]
        [Display(Name = "Severity")]
        public string SeverityId { get; set; }

        [StringLength(128)]
        [Display(Name = "Priority")]
        public string PriorityId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Status")]
        public string StatusId { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime CreateDateTime { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual Product Product { get; set; }

        public virtual Severity Severity { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BugReportAssignee> BugReportAssignees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BugReportSubscriber> BugReportSubscribers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
