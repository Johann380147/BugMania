namespace BugMania.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BugReportId { get; set; }

        [Required]
        public string EditorId { get; set; }

        public DateTime EditDateTime { get; set; }
        
        public int OperationId { get; set; }

        public Operation Operation { get; set; }

        public BugReport BugReport { get; set; }

        public ApplicationUser Editor { get; set; }
    }
}
