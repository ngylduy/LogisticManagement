using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public class LogisticDbContext : IdentityDbContext<AppUser>
    {
        public LogisticDbContext()
        {
        }

        public LogisticDbContext(DbContextOptions<LogisticDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Address>? Addresses { get; set; }
        public virtual DbSet<AppUser>? AppUsers { get; set; }
        public virtual DbSet<Parcel>? Parcels { get; set; }
        public virtual DbSet<ParcelGroups>? ParcelGroups { get; set; }
        public virtual DbSet<ParcelGroupItems>? ParcelGroupItems { get; set; }
        public virtual DbSet<DeliveryRoutes>? DeliveryRoutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasOne(a => a.Address)
                .WithMany(u => u.appUsers)
                .HasForeignKey(f => f.AddressId).OnDelete(DeleteBehavior.SetNull);

                entity.Property(e => e.FullName)
                    .HasMaxLength(255);

                entity.Property(e => e.AddressId).IsRequired(false);
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.DeliveryAddress)
                    .WithMany(p => p.deliveryParcel)
                    .HasForeignKey(d => d.DeliveryAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(p => p.PickupAddress)
                    .WithMany(p => p.pickupParcel)
                    .HasForeignKey(p => p.PickupAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(p => p.ReceiverUser)
                    .WithMany(p => p.ReceiverParcels)
                    .HasForeignKey(p => p.ReceiverUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(p => p.SenderUser)
                    .WithMany(p => p.SenderParcels)
                    .HasForeignKey(p => p.SenderUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ParcelGroups>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ParcelGroupItems>(entity =>
            {
                entity.HasKey(s => new { s.ParcelGroupId, s.ParcelId });

                entity.HasOne(s => s.ParcelGroup)
                    .WithMany(s => s.ParcelGroupItems)
                    .HasForeignKey(s => s.ParcelGroupId);

                entity.HasOne(s => s.ParcelItem)
                    .WithMany(s => s.ParcelGroupItems)
                    .HasForeignKey(s => s.ParcelId);
            });

            modelBuilder.Entity<DeliveryRoutes>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(a => a.Group)
                .WithMany(u => u.DeliveryRoutes)
                .HasForeignKey(f => f.GroupId).OnDelete(DeleteBehavior.SetNull);
            });

        }
    }
}
