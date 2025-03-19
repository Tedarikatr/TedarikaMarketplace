using Entity.Auths;
using Entity.Baskets;
using Entity.Carriers;
using Entity.Categories;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Markets;
using Entity.Orders;
using Entity.Payments;
using Entity.Products;
using Entity.Stores;
using Microsoft.EntityFrameworkCore;

namespace Data.Databases
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Kullanıcılar
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<BuyerUser> BuyerUsers { get; set; }
        public DbSet<SellerUser> SellerUsers { get; set; }

        // Sepet
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        // Kargo 
        public DbSet<Carrier> Carriers { get; set; }
        //Category
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySub> CategoriesSubs { get; set; }

        // Company
        public DbSet<Company> Companies { get; set; }

        //Ekbilgiler
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        //Market
        public DbSet<Market> Markets { get; set; }
        public DbSet<MarketCarrier> MarketCarriers { get; set; }

        //Siparişler
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Ödemeler & Faturalar
        public DbSet<Payment> Payments { get; set; }

        // Ürünler
        public DbSet<Product> Products { get; set; }

        // Mağaza & Marketler
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreCarrier> StoreCarriers { get; set; }
        public DbSet<StoreInvoice> StoreInvoices { get; set; }
        public DbSet<StoreMarket> StoreMarkets { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<StoreProductMarket> StoreProductMarkets { get; set; }
        public DbSet<StoreProductShippingRegion> StoreProductShippingRegions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreatingConfigurations.ApplyAllConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
