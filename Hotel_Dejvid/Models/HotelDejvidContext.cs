using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Dejvid.Models;

public partial class HotelDejvidContext : DbContext
{
    public HotelDejvidContext()
    {
    }

    public HotelDejvidContext(DbContextOptions<HotelDejvidContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Gost> Gosts { get; set; }

    public virtual DbSet<Placanje> Placanjes { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<Soba> Sobas { get; set; }

    public virtual DbSet<Zaposleni> Zaposlenis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=94.156.189.137;Initial Catalog=Hotel_Dejvid;User Id=sa;Password=HavilPavil21;Encrypt=No;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Gost>(entity =>
        {
            entity.ToTable("Gost");

            entity.Property(e => e.GostId)
                .ValueGeneratedOnAdd()
                .HasColumnName("GostId");
            entity.Property(e => e.BrojTelefona).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Prezime).HasMaxLength(50);

            entity.HasOne(d => d.GostNavigation).WithOne(p => p.Gost)
                .HasForeignKey<Gost>(d => d.GostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gost_Rezervacija");
        });

        modelBuilder.Entity<Placanje>(entity =>
        {
            entity.ToTable("Placanje");

            entity.Property(e => e.PlacanjeId).HasColumnName("PlacanjeID");
            entity.Property(e => e.Iznos).HasColumnType("money");
            entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaId");
            entity.Property(e => e.VrstaPlacanja).HasMaxLength(50);
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.ToTable("Rezervacija");

            entity.Property(e => e.RezervacijaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RezervacijaId");
            entity.Property(e => e.GostId).HasColumnName("GostId");
            entity.Property(e => e.SobaId).HasColumnName("SobaId");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.RezervacijaNavigation).WithOne(p => p.Rezervacija)
                .HasForeignKey<Rezervacija>(d => d.RezervacijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rezervacija_Placanje");
        });

        modelBuilder.Entity<Soba>(entity =>
        {
            entity.ToTable("Soba");

            entity.Property(e => e.SobaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SobaId");
            entity.Property(e => e.BrojSobe).HasMaxLength(50);
            entity.Property(e => e.CenaPoNoci).HasColumnType("money");
            entity.Property(e => e.TipSobe).HasMaxLength(50);

            entity.HasOne(d => d.SobaNavigation).WithOne(p => p.Soba)
                .HasForeignKey<Soba>(d => d.SobaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Soba_Rezervacija");
        });

        modelBuilder.Entity<Zaposleni>(entity =>
        {
            entity.ToTable("Zaposleni");

            entity.Property(e => e.ZaposleniId)
                .ValueGeneratedNever()
                .HasColumnName("ZaposleniId");
            entity.Property(e => e.BrojTelefona).HasMaxLength(50);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Pozicija).HasMaxLength(50);
            entity.Property(e => e.Prezime).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
