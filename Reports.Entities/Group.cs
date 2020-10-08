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
            GroupMembers = new HashSet<GroupMember>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }

        public virtual Product Product { get; set; }
    }
}
