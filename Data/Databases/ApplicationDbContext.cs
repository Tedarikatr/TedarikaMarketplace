using Entity.Auths;
using Entity.Baskets;
using Entity.Carriers;
using Entity.Categorys;
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
        public DbSet<BuyerUser> BuyerUsers { get; set; }
        public DbSet<SellerUser> SellerUsers { get; set; }

        // Şirketler
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }

        // Mağaza & Marketler
        public DbSet<Store> Stores { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<StoreMarket> StoreMarkets { get; set; }

        // Ürünler & Kategoriler
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMarket> ProductMarkets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySub> CategorySubs { get; set; }

        // Sepet & Siparişler
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Ödemeler & Faturalar
        public DbSet<Payment> Payments { get; set; }
        public DbSet<StoreInvoice> Invoices { get; set; }

        // Kargo & Teslimat
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<MarketCarrier> MarketCarriers { get; set; }
        public DbSet<StoreCarrier> StoreCarriers { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
