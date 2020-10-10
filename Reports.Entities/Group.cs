namespace Reports.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Members = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ApplicationUser> Members { get; set; }
    }
}
