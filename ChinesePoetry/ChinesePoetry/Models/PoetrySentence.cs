namespace ChinesePoetry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PoetrySentence")]
    public partial class PoetrySentence
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public string poetry { get; set; }

        public string author { get; set; }

        public long? poetryId { get; set; }

        public string image { get; set; }
    }
}
