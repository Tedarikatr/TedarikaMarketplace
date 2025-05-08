using Entity.Auths;
using Entity.Baskets;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Locations;
using Entity.Orders;
using Entity.Payments;
using Entity.Products;
using Entity.Stores;
using Entity.Stores.Markets;
using Entity.Stores.Payments;
using Entity.Stores.Products;
using Microsoft.EntityFrameworkCore;

namespace Data.Databases
{
    public static class ModelCreatingConfigurations
    {
        public static void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            ConfigureUserEntities(modelBuilder);
            ConfigureDeliveryAddressEntities(modelBuilder);
            ConfigureBasketEntities(modelBuilder);
            ConfigureCompanyEntities(modelBuilder);
            ConfigureStoreEntities(modelBuilder);
            ConfigureLocationEntities(modelBuilder);
            ConfigureProductEntities(modelBuilder);
            ConfigureOrderEntities(modelBuilder);
            ConfigurePaymentEntities(modelBuilder);
            ConfigureStoreMarketEntities(modelBuilder);
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

            modelBuilder.Entity<SellerUser>()
                .HasOne(s => s.Store)
                .WithOne(st => st.SellerUser)
                .HasForeignKey<Store>(st => st.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        private static void ConfigureDeliveryAddressEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.BuyerUser)
                .WithMany()
                .HasForeignKey(d => d.BuyerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.Country)
                .WithMany()
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.State)
                .WithMany()
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.District)
                .WithMany()
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(d => d.Neighborhood)
                .WithMany()
                .HasForeignKey(d => d.NeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        private static void ConfigureBasketEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.Items)
                .WithOne(i => i.Basket)
                .HasForeignKey(i => i.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Basket>()
                .Property(b => b.Currency)
                .HasMaxLength(10);
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
                .HasOne(s => s.SellerUser)
                .WithOne(o => o.Store)
                .HasForeignKey<Store>(s => s.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreProduct>()
                .HasOne(sp => sp.Store)
                .WithMany(s => s.StoreProducts)
                .HasForeignKey(sp => sp.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureLocationEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Country>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Provinces)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Province>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Province>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Province>()
                .HasMany(p => p.Districts)
                .WithOne(d => d.Province)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<District>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<District>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<District>()
                .HasMany(d => d.Neighborhoods)
                .WithOne(n => n.District)
                .HasForeignKey(n => n.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Neighborhood>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Neighborhood>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<State>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<State>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<State>()
                .HasMany(s => s.Provinces)
                .WithOne(p => p.State)
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<State>()
                .HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.CountryId)
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

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany()
                .HasForeignKey(o => o.DeliveryAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.SelectedCarrier)
                .WithMany()
                .HasForeignKey(o => o.SelectedCarrierId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne()
                .HasForeignKey<Order>(o => o.PaymentId)
                .OnDelete(DeleteBehavior.SetNull);

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
                .HasKey(p => p.Id);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Currency)
                .HasMaxLength(10);

            modelBuilder.Entity<Payment>()
                .Property(p => p.OrderNumber)
                .HasMaxLength(50);
        }

        private static void ConfigureStoreMarketEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreMarketCountry>()
                .HasOne(smc => smc.Store)
                .WithMany(s => s.MarketCountries)
                .HasForeignKey(smc => smc.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketCountry>()
                .HasOne(smc => smc.Country)
                .WithMany()
                .HasForeignKey(smc => smc.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarketProvince>()
                .HasOne(smp => smp.Store)
                .WithMany(s => s.MarketProvinces)
                .HasForeignKey(smp => smp.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketProvince>()
                .HasOne(smp => smp.Province)
                .WithMany()
                .HasForeignKey(smp => smp.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarketDistrict>()
                .HasOne(smd => smd.Store)
                .WithMany(s => s.MarketDistricts)
                .HasForeignKey(smd => smd.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketDistrict>()
                .HasOne(smd => smd.District)
                .WithMany()
                .HasForeignKey(smd => smd.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarketNeighborhood>()
                .HasOne(smn => smn.Store)
                .WithMany(s => s.MarketNeighborhoods)
                .HasForeignKey(smn => smn.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketNeighborhood>()
                .HasOne(smn => smn.Neighborhood)
                .WithMany()
                .HasForeignKey(smn => smn.NeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarketState>()
                .HasOne(sms => sms.Store)
                .WithMany(s => s.MarketStates)
                .HasForeignKey(sms => sms.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketState>()
                .HasOne(sms => sms.State)
                .WithMany()
                .HasForeignKey(sms => sms.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreMarketRegion>()
                .HasOne(smr => smr.Store)
                .WithMany(s => s.MarketRegions)
                .HasForeignKey(smr => smr.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreMarketRegion>()
                .HasOne(smr => smr.Region)
                .WithMany()
                .HasForeignKey(smr => smr.RegionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureDecimal(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().Property(b => b.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BasketItem>().Property(bi => bi.TotalPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BasketItem>().Property(bi => bi.UnitPrice).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Ignore(oi => oi.TotalPrice); 

            modelBuilder.Entity<Payment>().Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>().Property(p => p.PaidPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>().Property(p => p.PaidAmount).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<StoreInvoice>().Property(si => si.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<StoreProduct>().Property(sp => sp.UnitPrice).HasColumnType("decimal(18,2)");
        }

    }
}
