using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestHotelSmart.WebApi.Model;

public partial class TestHotelContext : DbContext
{
    public TestHotelContext()
    {
    }

    public TestHotelContext(DbContextOptions<TestHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> City { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<Documenttype> Documenttype { get; set; }

    public virtual DbSet<Gender> Gender { get; set; }

    public virtual DbSet<Guest> Guest { get; set; }

    public virtual DbSet<Hotel> Hotel { get; set; }

    public virtual DbSet<Reservation> Reservation { get; set; }

    public virtual DbSet<Room> Room { get; set; }

    public virtual DbSet<Roomtype> Roomtype { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name = PruebaSmart");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CITY__3214EC077421C55D");

            entity.ToTable("CITY");

            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Section)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.DocumentNumber).HasName("PK__CUSTOMER__689939195279989B");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.Customer)
                .HasForeignKey(d => d.IdDocumentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdDocumentType_fk");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Customer)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdGender_fk");
        });

        modelBuilder.Entity<Documenttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DOCUMENT__3214EC07BD8DA468");

            entity.ToTable("DOCUMENTTYPE");

            entity.Property(e => e.DocumentDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GENDER__3214EC07AFA4A154");

            entity.ToTable("GENDER");

            entity.Property(e => e.GenderDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GUEST__3214EC073945161E");

            entity.ToTable("GUEST");

            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentNumberNavigation).WithMany(p => p.Guest)
                .HasForeignKey(d => d.DocumentNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GuestDocumentNumber_fk");

            entity.HasOne(d => d.IdReservationNavigation).WithMany(p => p.Guest)
                .HasForeignKey(d => d.IdReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdReservation_fk");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HOTEL__3214EC07EC226C3E");

            entity.ToTable("HOTEL");

            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Hotel)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdCity_fk");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESERVAT__3214EC0736B4104E");

            entity.ToTable("RESERVATION");

            entity.Property(e => e.ArrivalDate).HasColumnType("datetime");
            entity.Property(e => e.DepartureDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactFullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactPhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentNumberNavigation).WithMany(p => p.Reservation)
                .HasForeignKey(d => d.DocumentNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReservationDocumentNumber_fk");

            entity.HasOne(d => d.IdRoomNavigation).WithMany(p => p.Reservation)
                .HasForeignKey(d => d.IdRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdRoom_fk");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM__3214EC073AE94DB8");

            entity.ToTable("ROOM");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RoomName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Taxes).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Room)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdHotel_fk");

            entity.HasOne(d => d.IdRoomTypeNavigation).WithMany(p => p.Room)
                .HasForeignKey(d => d.IdRoomType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdRoomType_fk");
        });

        modelBuilder.Entity<Roomtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMTYPE__3214EC0798FB4C8E");

            entity.ToTable("ROOMTYPE");

            entity.Property(e => e.RoomDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
