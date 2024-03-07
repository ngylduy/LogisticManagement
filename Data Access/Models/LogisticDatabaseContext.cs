using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data_Access.Models
{
    public partial class LogisticDatabaseContext : DbContext
    {
        public LogisticDatabaseContext()
        {
        }

        public LogisticDatabaseContext(DbContextOptions<LogisticDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Parcel> Parcels { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("postalcode");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.ToTable("parcels");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Customeraddress).HasColumnName("customeraddress");

                entity.Property(e => e.Customername)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customername");

                entity.Property(e => e.Customerphone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customerphone");

                entity.Property(e => e.Dimensions)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dimensions");

                entity.Property(e => e.Sellerid).HasColumnName("sellerid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.CustomeraddressNavigation)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.Customeraddress)
                    .HasConstraintName("ParcelCustomerAddress");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.Sellerid)
                    .HasConstraintName("ParcelUser");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shipper");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.AreaNavigation)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.Area)
                    .HasConstraintName("ShiperAddress");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shippers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("ShiperUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Statuts).HasColumnName("statuts");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.AddressNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Address)
                    .HasConstraintName("UserAddress");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
