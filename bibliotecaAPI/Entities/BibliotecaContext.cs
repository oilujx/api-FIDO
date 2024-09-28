using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAPI.Entities;

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext()
    {
    }

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=database");
        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("autor", tb => tb.HasComment("		"));

            entity.HasIndex(e => e.Nombre, "nombre_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("estado");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecCreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("editorial");

            entity.HasIndex(e => e.Nombre, "nombre_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("estado");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecCreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(450)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PRIMARY");

            entity.ToTable("libro");

            entity.HasIndex(e => e.Autor, "fk_autor_idx");

            entity.HasIndex(e => e.Editorial, "fk_editorial_idx");

            entity.Property(e => e.Isbn).HasColumnName("isbn");
            entity.Property(e => e.AnioEdicion).HasColumnName("anioEdicion");
            entity.Property(e => e.Autor).HasColumnName("autor");
            entity.Property(e => e.Editorial).HasColumnName("editorial");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("estado");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecCreacion");
            entity.Property(e => e.PrecioPrestamo)
                .HasPrecision(6)
                .HasColumnName("precioPrestamo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(450)
                .HasColumnName("titulo");

            entity.HasOne(d => d.AutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Autor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_autor");

            entity.HasOne(d => d.EditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Editorial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_editorial");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prestamo");

            entity.HasIndex(e => e.Libro, "fk_prestamo_libro_idx");

            entity.HasIndex(e => e.Socio, "fk_prestamo_socio_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("estado");
            entity.Property(e => e.FecDevolucion)
                .HasColumnType("datetime")
                .HasColumnName("fecDevolucion");
            entity.Property(e => e.FecPrestamo)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecPrestamo");
            entity.Property(e => e.Libro).HasColumnName("libro");
            entity.Property(e => e.Socio).HasColumnName("socio");
            entity.Property(e => e.TotalPrestamo)
                .HasPrecision(6)
                .HasColumnName("totalPrestamo");

            entity.HasOne(d => d.LibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Libro)
                .HasConstraintName("fk_prestamo_libro");

            entity.HasOne(d => d.SocioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Socio)
                .HasConstraintName("fk_prestamo_socio");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("socio");

            entity.HasIndex(e => e.Correo, "correo_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(45)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("estado");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecCreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
