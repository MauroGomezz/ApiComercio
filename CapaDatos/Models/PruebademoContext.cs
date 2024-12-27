using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Models;

public partial class PruebademoContext : DbContext
{
    public PruebademoContext()
    {
    }

    public PruebademoContext(DbContextOptions<PruebademoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<Ventasitem> Ventasitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3214EC2744B7150D");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Cliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3214EC27F2E810F7");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ventas__3214EC2784DEA4B3");

            entity.ToTable("ventas");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("FK__ventas__IDClient__0B91BA14");
        });

        modelBuilder.Entity<Ventasitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ventasit__3214EC27666F297F");

            entity.ToTable("ventasitems");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Ventasitems)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__ventasite__IDPro__114A936A");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.Ventasitems)
                .HasForeignKey(d => d.Idventa)
                .HasConstraintName("FK__ventasite__IDVen__10566F31");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
