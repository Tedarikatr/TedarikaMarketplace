using Entity.Auths;
using Entity.Baskets;
using Entity.Companies;
using Entity.Orders;
using Entity.Payments;
using Entity.Products;
using Entity.Stores;
using Microsoft.EntityFrameworkCore;

namespace Data.Databases
{
    public static class ModelCreatingConfigurations
    {
        public static void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            ConfigureUserEntities(modelBuilder);
            ConfigureCompanyEntities(modelBuilder);
            ConfigureStoreEntities(modelBuilder);
            ConfigureProductEntities(modelBuilder);
            ConfigureOrderEntities(modelBuilder);
            ConfigurePaymentEntities(modelBuilder);
            ConfigureDecimal(modelBuilder);
        }

        private static void ConfigureUserEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyerUser>()
                .HasOne(b => b.Company)
                .WithOne(c => c.BuyerUser)
                .HasForeignKey<Company>(c => c.BuyerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SellerUser>()
                .HasOne(s => s.Company)
                .WithOne(c => c.SellerUser)
                .HasForeignKey<Company>(c => c.SellerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureCompanyEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasIndex(c => c.CompanyNumber).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(c => c.TaxNumber).IsUnique();

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Stores)
                .WithOne(s => s.Company)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureStoreEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Owner)
                .WithMany(o => o.Store)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarket>()
                .HasOne(sm => sm.Store)
                .WithMany(s => s.StoreMarkets)
                .HasForeignKey(sm => sm.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreProduct>()
                .HasOne(sp => sp.Store)
                .WithMany(s => s.StoreProducts)
                .HasForeignKey(sp => sp.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureProductEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => p.ProductNumber).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreProductMarket>()
                .HasOne(spm => spm.StoreProduct)
                .WithMany(sp => sp.ProductMarkets)
                .HasForeignKey(spm => spm.StoreProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureOrderEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany()
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigurePaymentEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureDecimal(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().Property(b => b.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BasketItem>().Property(bi => bi.TotalPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BasketItem>().Property(bi => bi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<StoreInvoice>().Property(si => si.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<StoreProduct>().Property(sp => sp.Price).HasColumnType("decimal(18,2)");
        }

    }
}
