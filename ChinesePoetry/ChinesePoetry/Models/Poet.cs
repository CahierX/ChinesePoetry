namespace ChinesePoetry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Poet")]
    public partial class Poet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long poetId { get; set; }

        public string objectId { get; set; }

        public int? star { get; set; }

        [StringLength(20)]
        public string name { get; set; }

        public string image { get; set; }

        [StringLength(10)]
        public string dynasty { get; set; }

        public string minIntroduce { get; set; }

        public string introduce { get; set; }
    }
}
