using Entity.Anohter;
using Entity.Auths;
using Entity.Baskets;
using Entity.Carriers;
using Entity.Categories;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Forms;
using Entity.Incoterms;
using Entity.Locations;
using Entity.Orders;
using Entity.Payments;
using Entity.Products;
using Entity.Stores;
using Entity.Stores.Carriers;
using Entity.Stores.Locations;
using Entity.Stores.Payments;
using Entity.Stores.Products;
using Microsoft.EntityFrameworkCore;

namespace Data.Databases
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        //Anohtrer
        public DbSet<EstimatedExportCost> EstimatedExportCosts { get; set; }

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

        //DeliveryAddress
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        //Forms
        public DbSet<SellerApplication> SellerApplications { get; set; }
        public DbSet<BuyerApplication> BuyerApplications { get; set; }

        //Incornem
        public DbSet<Incoterm> Incoterms { get; set; }

        //Locations
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<State> States { get; set; }

        //Siparişler
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Ödemeler & Faturalar
        public DbSet<Payment> Payments { get; set; }

        // Ürünler
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductExportBanned> ProductExportBanneds { get; set; }

        // Mağaza & Marketler
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreCertificate> StoreCertificates { get; set; }

        public DbSet<StoreCarrier> StoreCarriers { get; set; }

        public DbSet<StoreLocationCountry> StoreLocationCountries { get; set; }
        public DbSet<StoreLocationCoverage> StoreLocationCoverages { get; set; }
        public DbSet<StoreLocationDistrict> StoreLocationDistricts { get; set; }
        public DbSet<StoreLocationNeighborhood> StoreLocationNeighborhoods { get; set; }
        public DbSet<StoreLocationProvince> StoreLocationProvinces { get; set; }
        public DbSet<StoreLocationRegion> StoreLocationRegions { get; set; }
        public DbSet<StoreLocationState> StoreLocationStates { get; set; }

        public DbSet<StoreInvoice> StoreInvoices { get; set; }
        public DbSet<StorePaymentMethod> StorePaymentMethods { get; set; }

        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<StoreProductRequest> StoreProductRequests { get; set; }
        public DbSet<StoreProductShippingRegion> StoreProductShippingRegions { get; set; }
        public DbSet<StoreProductCertificate> StoreProductCertificates { get; set; }
        public DbSet<StoreProductIncoterm> StoreProductIncoterms { get; set; }
        public DbSet<StoreProductShowroom> StoreProductShowrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreatingConfigurations.ApplyAllConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
