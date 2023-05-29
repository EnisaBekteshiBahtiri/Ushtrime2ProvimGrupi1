using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ushtrime2ProvimGrupi1.Entities
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autori> Autoris { get; set; }
        public virtual DbSet<Kategorium> Kategoria { get; set; }
        public virtual DbSet<Shkrimet> Shkrimets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-8EOIU82;Initial Catalog=Ushtrime2ProvimGrupi1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Autori>(entity =>
            {
                entity.ToTable("Autori");

                entity.Property(e => e.Emri)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Mbiemri)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Kategorium>(entity =>
            {
                entity.Property(e => e.Emri)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Shkrimet>(entity =>
            {
                entity.ToTable("Shkrimet");

                entity.Property(e => e.Titulli)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Autori)
                    .WithMany(p => p.Shkrimets)
                    .HasForeignKey(d => d.AutoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shkrimet_Autori");

                entity.HasOne(d => d.Kategoria)
                    .WithMany(p => p.Shkrimets)
                    .HasForeignKey(d => d.KategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shkrimet_Kategoria");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
