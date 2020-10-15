using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugMania.Shapes
{
    public class Operation
    {
        public Operation()
        {
            Logs = new HashSet<Log>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Log> Logs { get; set; }
    }
}
