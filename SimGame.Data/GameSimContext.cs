using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data
{

    public class GameSimContext : AbstractContext, IGameSimContext
    {
        public GameSimContext()
            : base("GameSimContextConnection")
        {
        }

        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Manufacturer> Manufacturers { get; set; }
        public IDbSet<ManufacturerType> ManufacturerTypes { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<BuildingUpgrade> BuildingUpgrades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .HasRequired(x => x.ManufacturerType)
                .WithMany(y=>y.Manufacturers)
                .HasForeignKey(z=>z.ManufacturerTypeId);
            modelBuilder.Entity<ManufacturerType>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<ProductType>()
                .HasRequired(y => y.ManufacturerType)
                .WithMany(z => z.ProductTypes)
                .HasForeignKey(x => x.ManufacturerTypeId);
            modelBuilder.Entity<ProductType>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Product>()
                .HasRequired(y => y.ProductType)
                .WithMany(z => z.Products)
                .HasForeignKey(x => x.ProductTypeId);

            modelBuilder.Entity<Product>()
                .HasOptional(y => y.BuildingUpgrade)
                .WithMany(z => z.Products)
                .HasForeignKey(x => x.BuildingUpgradeId);

            modelBuilder.Entity<Product>()
                .HasOptional(x => x.RequiredBy)
                .WithMany(y => y.RequiredProducts)
                .HasForeignKey(z => z.RequiredByTypeId);

            modelBuilder.Entity<Product>()
                .HasOptional(y => y.Order)
                .WithMany(z => z.Products)
                .HasForeignKey(x => x.OrderId);
            
            modelBuilder.Entity<ManufacturingQueueSlot>()
                .HasOptional(t => t.Manufacturer)
                .WithMany(x=>x.ManufacturingQueueSlots)
                .HasForeignKey(z=>z.ManufacturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManufacturingQueueSlot>()
                .HasOptional(x => x.Product)
                .WithMany(y => y.ManufacturingQueues)
                .HasForeignKey(z=>z.ProductId)
                .WillCascadeOnDelete(false);



        }
    }
}
