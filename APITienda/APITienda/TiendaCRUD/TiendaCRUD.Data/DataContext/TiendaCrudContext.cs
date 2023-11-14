using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.DataContext;

public partial class TiendaCrudContext : DbContext
{
    public TiendaCrudContext()
    {
    }

    public TiendaCrudContext(DbContextOptions<TiendaCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Tiendum> Tienda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }/*
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Server=(local); DataBase=TiendaCRUD; Trusted_Connection=True; TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PK__Articulo__F8FF5D52FFFF378B");

            entity.ToTable("Articulo");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdTienda)
                .HasConstraintName("FK__Articulo__IdTien__47DBAE45");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__Carrito__8B4A618CAA5C589D");

            entity.ToTable("Carrito");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdArticulo)
                .HasConstraintName("FK__Carrito__IdArtic__2B3F6F97");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Carrito__IdClien__2A4B4B5E");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdTienda)
                .HasConstraintName("FK__Carrito__IdTiend__2C3393D0");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D5946642EEFD2468");

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Dirección).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__0A5CDB5C4FFDFEB3");

            entity.ToTable("Compra");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdArticulo)
                .HasConstraintName("FK__Compra__IdArticu__2F10007B");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Compra__IdClient__300424B4");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdTienda)
                .HasConstraintName("FK__Compra__IdTienda__30F848ED");
        });

        modelBuilder.Entity<Tiendum>(entity =>
        {
            entity.HasKey(e => e.IdTienda).HasName("PK__Tienda__5A1EB96B4001A643");

            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Sucursal).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
