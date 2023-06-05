using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarteleraCine.Models;

public partial class CarteleraCineContext : DbContext
{
    public CarteleraCineContext()
    {
    }

    public CarteleraCineContext(DbContextOptions<CarteleraCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseSqlServer("server=localhost; database=cartelera_cine; Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__director__3213E83FAA9CFF0E");

            entity.ToTable("director");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83FE013B7DC");

            entity.ToTable("genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pelicula__3213E83F75D4EDCA");

            entity.ToTable("peliculas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.Imagen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Puntos).HasColumnName("puntos");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdDirectorNavigation).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdDirector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_peliculas_director");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_peliculas_genero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
