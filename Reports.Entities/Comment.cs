namespace Reports.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int BugReportId { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime CommentDateTime { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        
        [Required]
        public BugReport BugReport { get; set; }


        public ApplicationUser Commenter { get; set; }
    }
}
