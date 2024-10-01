using System;
using System.Collections.Generic;
using apiFIDO.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace apiFIDO.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CanjePremio> CanjePremios { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Codigo> Codigos { get; set; }

    public virtual DbSet<CodigoPremio> CodigoPremios { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<RazaPerro> RazaPerros { get; set; }

    public virtual DbSet<VwValidaCodigo> VwValidaCodigos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=prodevsol.c3ue088ogzkd.us-east-1.rds.amazonaws.com;database=FIDOAWS;user=admin;password=prodevsol24!", ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<CanjePremio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CANJE_PREMIO");

            entity.HasIndex(e => e.IdCliente, "FK_CLIENTE_idx");

            entity.HasIndex(e => e.IdCodigoPremio, "FK_CODIGO_PREMIO_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FecCanje)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("FEC_CANJE");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdCodigoPremio).HasColumnName("ID_CODIGO_PREMIO");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CanjePremios)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_CLIENTE");

            entity.HasOne(d => d.IdCodigoPremioNavigation).WithMany(p => p.CanjePremios)
                .HasForeignKey(d => d.IdCodigoPremio)
                .HasConstraintName("FK_CODIGO_PREMIO");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => e.IdRaza, "FK_RAZA_idx");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .HasColumnName("CORREO");
            entity.Property(e => e.Direccion)
                .HasMaxLength(1000)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.IdRaza).HasColumnName("ID_RAZA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.PesoPerro)
                .HasPrecision(10)
                .HasColumnName("PESO_PERRO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(8)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdRazaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRaza)
                .HasConstraintName("FK_RAZA");
        });

        modelBuilder.Entity<Codigo>(entity =>
        {
            entity.HasKey(e => e.IdCodigo).HasName("PRIMARY");

            entity.ToTable("CODIGO");

            entity.HasIndex(e => e.Codigo1, "CODIGO_UNIQUE").IsUnique();

            entity.Property(e => e.IdCodigo).HasColumnName("ID_CODIGO");
            entity.Property(e => e.Codigo1)
                .HasMaxLength(500)
                .HasColumnName("CODIGO");
        });

        modelBuilder.Entity<CodigoPremio>(entity =>
        {
            entity.HasKey(e => e.IdCodigoPremio).HasName("PRIMARY");

            entity.ToTable("CODIGO_PREMIO");

            entity.HasIndex(e => e.IdCodigo, "FK_CODIGO_idx");

            entity.HasIndex(e => e.IdPremio, "FK_PREMIO_idx");

            entity.Property(e => e.IdCodigoPremio).HasColumnName("ID_CODIGO_PREMIO");
            entity.Property(e => e.IdCodigo).HasColumnName("ID_CODIGO");
            entity.Property(e => e.IdPremio).HasColumnName("ID_PREMIO");

            entity.HasOne(d => d.IdCodigoNavigation).WithMany(p => p.CodigoPremios)
                .HasForeignKey(d => d.IdCodigo)
                .HasConstraintName("FK_CODIGO");

            entity.HasOne(d => d.IdPremioNavigation).WithMany(p => p.CodigoPremios)
                .HasForeignKey(d => d.IdPremio)
                .HasConstraintName("FK_PREMIO");
        });

        modelBuilder.Entity<Premio>(entity =>
        {
            entity.HasKey(e => e.IdPremio).HasName("PRIMARY");

            entity.ToTable("PREMIO");

            entity.Property(e => e.IdPremio).HasColumnName("ID_PREMIO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Src)
                .HasMaxLength(2500)
                .HasColumnName("SRC");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("TITULO");
        });

        modelBuilder.Entity<RazaPerro>(entity =>
        {
            entity.HasKey(e => e.IdRaza).HasName("PRIMARY");

            entity.ToTable("RAZA_PERRO");

            entity.Property(e => e.IdRaza).HasColumnName("ID_RAZA");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("ESTADO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<VwValidaCodigo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_VALIDA_CODIGO");

            entity.Property(e => e.Codigo)
                .HasMaxLength(500)
                .HasColumnName("CODIGO");
            entity.Property(e => e.IndCanjeado)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .HasColumnName("IND_CANJEADO")
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
