namespace gdoc.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloDados : DbContext
    {
        public ModeloDados()
            : base("name=ModeloDados")
        {
        }

        public virtual DbSet<Documento> Documento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>()
                .Property(e => e.nomeDocumento)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.tipoDocumento)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.observacaoDocumento)
                .IsUnicode(false);
        }
    }
}
