namespace ChinesePoetry.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Poetry")]
    public partial class Poetry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poetry()
        {
            Collect = new HashSet<Collect>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long poetryId { get; set; }

        public int? star { get; set; }

        public string name { get; set; }

        [StringLength(10)]
        public string dynasty { get; set; }

        public string poetryContent { get; set; }

        public string fanyi { get; set; }

        public string shangxi { get; set; }

        public string author { get; set; }

        public string about { get; set; }

        public string tags { get; set; }

        public string poetryForBaidu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collect> Collect { get; set; }
    }
}
