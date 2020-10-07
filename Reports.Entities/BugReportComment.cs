namespace Reports.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BugReportComment
    {
        [Key]
        [Column(Order = 0)]
        public string BugReportId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string CommentId { get; set; }
    }
}
