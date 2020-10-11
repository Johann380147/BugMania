namespace Reports.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BugReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BugReport()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            Subscribers = new HashSet<ApplicationUser>();
            Assignees = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Product")]
        public int ProductId { get; set; }
        
        [StringLength(128)]
        public string AuthorId { get; set; }
        
        [Display(Name = "Severity")]
        public int SeverityId { get; set; }
        
        [Display(Name = "Priority")]
        public int PriorityId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime CreateDateTime { get; set; }
        
        public Product Product { get; set; }

        public Severity Severity { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public ApplicationUser Author { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Tag> Tags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ApplicationUser> Subscribers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ApplicationUser> Assignees { get; set; }
    }
}
