namespace BugMania.Shapes
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
            GroupMembers = new HashSet<GroupMember>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<GroupMember> GroupMembers { get; set; }
    }
}
