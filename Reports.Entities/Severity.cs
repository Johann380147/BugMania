namespace BugMania.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Severity")]
    public partial class Severity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Severity()
        {
            BugReports = new HashSet<BugReport>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Severity")]
        public string Name { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<BugReport> BugReports { get; set; }
    }
}
