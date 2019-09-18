namespace ChuyenData.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelV2 : DbContext
    {
        public ModelV2()
            : base("name=ModelV2")
        {
        }

        public virtual DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
