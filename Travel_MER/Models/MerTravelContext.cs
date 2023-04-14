using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Travel_MER.Models;

public partial class MerTravelContext : DbContext
{
    public MerTravelContext()
    {
    }

    public MerTravelContext(DbContextOptions<MerTravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=MER_TRAVEL; integrated security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autor__3214EC07A0B88AC1");

            entity.ToTable("Autor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.LibroIsbns).WithMany(p => p.Autors)
                .UsingEntity<Dictionary<string, object>>(
                    "AutorHasLibro",
                    r => r.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroIsbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Autor_Has__Libro__3D5E1FD2"),
                    l => l.HasOne<Autor>().WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Autor_Has__Autor__3C69FB99"),
                    j =>
                    {
                        j.HasKey("AutorId", "LibroIsbn");
                        j.ToTable("Autor_Has_Libro");
                        j.IndexerProperty<int>("AutorId").HasColumnName("Autor_Id");
                        j.IndexerProperty<long>("LibroIsbn").HasColumnName("Libro_ISBN");
                    });
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Editoria__3214EC073124BBD0");

            entity.ToTable("Editorial");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Sede)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Libro__447D36EBE16D0CB3");

            entity.ToTable("Libro");

            entity.Property(e => e.Isbn)
                .ValueGeneratedNever()
                .HasColumnName("ISBN");
            entity.Property(e => e.EditorialId).HasColumnName("Editorial_Id");
            entity.Property(e => e.NPaginas)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("N_Paginas");
            entity.Property(e => e.Sinopsis).HasColumnType("text");
            entity.Property(e => e.Titulo)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
