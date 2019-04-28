namespace ChinesePoetry.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Db : DbContext
    {
        public Db()
            : base("name=Db")
        {
        }

        public virtual DbSet<Collect> Collect { get; set; }
        public virtual DbSet<Poet> Poet { get; set; }
        public virtual DbSet<Poetry> Poetry { get; set; }
        public virtual DbSet<PoetrySentence> PoetrySentence { get; set; }
        public virtual DbSet<UserList> UserList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poetry>()
                .HasMany(e => e.Collect)
                .WithRequired(e => e.Poetry)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserList>()
                .HasMany(e => e.Collect)
                .WithOptional(e => e.UserList)
                .HasForeignKey(e => e.userId);
        }
    }
}
