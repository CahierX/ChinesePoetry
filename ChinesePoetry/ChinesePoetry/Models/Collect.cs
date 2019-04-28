namespace ChinesePoetry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Collect")]
    public partial class Collect
    {
        public long id { get; set; }

        public long? userId { get; set; }

        public long poetryId { get; set; }

        public DateTime time { get; set; }

        public virtual Poetry Poetry { get; set; }

        public virtual UserList UserList { get; set; }
    }
}
