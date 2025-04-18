using Data.Databases;
using Entity.Auths;
using Entity.Categories;
using Entity.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Seeders
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(ApplicationDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Database seeding başladı.");

                // SuperAdmin kontrolü
                if (!await _context.AdminUsers.AnyAsync(u => u.IsSuperAdmin))
                {
                    var superAdminSalt = BCrypt.Net.BCrypt.GenerateSalt();
                    var superAdmin = new AdminUser
                    {
                        Name = "Sistem",
                        LastName = "Yöneticisi",
                        Email = "superadmin@tedarika.com",
                        Phone = "5555555555",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("SuperAdmin123", superAdminSalt),
                        PasswordSalt = superAdminSalt,
                        IsSuperAdmin = true,
                        UserType = UserType.Admin,
                        Role = AdminRole.SuperAdmin
                    };

                    await _context.AdminUsers.AddAsync(superAdmin);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("SuperAdmin başarıyla oluşturuldu. Email: {Email}", superAdmin.Email);
                }
                else
                {
                    _logger.LogInformation("SuperAdmin zaten mevcut, ekleme yapılmadı.");
                }

                // Standart Admin kontrolü
                if (!await _context.AdminUsers.AnyAsync(u => !u.IsSuperAdmin))
                {
                    var adminSalt = BCrypt.Net.BCrypt.GenerateSalt();
                    var admin = new AdminUser
                    {
                        Name = "Standart",
                        LastName = "Admin",
                        Email = "admin@tedarika.com",
                        Phone = "5444444444",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123", adminSalt),
                        PasswordSalt = adminSalt,
                        IsSuperAdmin = false,
                        UserType = UserType.Admin,
                        Role = AdminRole.StandardAdmin
                    };

                    await _context.AdminUsers.AddAsync(admin);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Standart Admin başarıyla oluşturuldu. Email: {Email}", admin.Email);
                }
                else
                {
                    _logger.LogInformation("Standart Admin zaten mevcut, ekleme yapılmadı.");
                }

                // Kategoriler
                if (!await _context.Categories.AnyAsync())
                {
                    var categories = new List<Category>
                    {
                        new Category 
                        { 
                            CategoryName = "Gıda", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/993f2e01-f1d0-43a3-bc68-a5d9886da563.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Temel Gıda" },
                                new CategorySub { Name = "Konserve ve Hazır Gıdalar" },
                                new CategorySub { Name = "Taze" },
                                new CategorySub { Name = "Süt ve Süt Ürünleri" },
                                new CategorySub { Name = "İçecekler" },
                                new CategorySub { Name = "Dondurulmuş Gıdalar" },
                                new CategorySub { Name = "Atıştırmalıklar ve Tatlılar" }
                            }
                        },
                        new Category 
                        { CategoryName = "Temizlik", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/b9724622-a2f8-4256-b5cd-38886943c7d6.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Ev Temizlik" },
                                new CategorySub { Name = "Endüstriyel Temizlik" },
                                new CategorySub { Name = "Kişisel Hijyen" },
                                new CategorySub { Name = "Hijyen" },
                                new CategorySub { Name = "Temizlik Ekipmanları" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Kişisel Bakım", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/5773597e-47a9-42e7-9602-496cc3caaf52.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Saç Bakımı" },
                                new CategorySub { Name = "Cilt Bakımı" },
                                new CategorySub { Name = "Ağız ve Diş Bakımı" },
                                new CategorySub { Name = "Vücut Bakımı" },
                                new CategorySub { Name = "Kozmetik" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Moda ve Aksesuarlar", CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Giyim" },
                                new CategorySub { Name = "Ayakkabı" },
                                new CategorySub { Name = "Çanta ve Aksesuarlar" },
                                new CategorySub { Name = "Mücevherat ve Takılar" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Kırtasiye ve Ofis", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/81feb1d3-aabf-4fdc-b9a0-85e2fc74107d.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Yazı ve Çizim" },
                                new CategorySub { Name = "Kâğıt Ürünleri" },
                                new CategorySub { Name = "Ofis Ekipmanları" },
                                new CategorySub { Name = "Dosyalama ve Arşivleme" },
                                new CategorySub { Name = "Ofis Mobilyaları" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Tekstil", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/5f1f2971-1d4f-4258-818b-5a01ba1345e9.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Ev Tekstili" },
                                new CategorySub { Name = "Mutfak Tekstili" },
                                new CategorySub { Name = "Banyo Tekstili" },
                                new CategorySub { Name = "Giyim" },
                                new CategorySub { Name = "Oteller ve HORECA Tekstili" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Elektronik", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/cc635a51-085d-4fd8-9825-44b2b86d5b46.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Ev Elektroniği" },
                                new CategorySub { Name = "Küçük Ev Aletleri" },
                                new CategorySub { Name = "Bilgisayar ve Aksesuarlar" },
                                new CategorySub { Name = "Telefon ve Aksesuarlar" },
                                new CategorySub { Name = "Aydınlatma" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Sağlık ve Medikal", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/462d8ccd-dec1-40a5-b12f-045e80268408.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "Tıbbi Malzemeler" },
                                new CategorySub { Name = "Tıbbi Cihazlar" },
                                new CategorySub { Name = "Koruyucu Ekipmanlar" },
                                new CategorySub { Name = "İlaç ve Vitaminler" }
                            }
                        },
                        new Category 
                        { 
                            CategoryName = "Mobilya ve Dekorasyon", CategoryImage = "https://todayapipictures.blob.core.windows.net/sector/e22ed51d-5127-4229-a22a-5e86f29bf774.png",
                            CategoriesSubs = new List<CategorySub>
                            {
                                new CategorySub { Name = "İç Mekân Mobilyaları" },
                                new CategorySub { Name = "Dış Mekân Mobilyaları" },
                                new CategorySub { Name = "Dekoratif Ürünler" },
                                new CategorySub { Name = "Ofis Mobilyaları" }
                            }
                        }
                    };


                    await _context.Categories.AddRangeAsync(categories);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Tüm kategoriler başarıyla eklendi.");
                }
                else
                {
                    _logger.LogInformation("Kategori verileri zaten mevcut, ekleme yapılmadı.");
                }

                //Products
                if (!await _context.Products.AnyAsync())
                {
                    var products = new List<Product>
                    {
                        new Product { Name = "Ayçiçek Yağı", Description = "5 Litre kaliteli ayçiçek yağı.", UnitType = UnitType.Litre, ProductNumber = "GD001", Barcode = "8680000000001", Brand = "BizimYağ", ImageUrl = "imageurl1", CategoryId = 1, CategoryName = "Gıda", CategorySubId = 1, CategorySubName = "Temel Gıda", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Konserve Bezelye", Description = "400gr bezelye konservesi.", UnitType = UnitType.Adet, ProductNumber = "GD002", Barcode = "8680000000002", Brand = "Tat", ImageUrl = "imageurl2", CategoryId = 1, CategoryName = "Gıda", CategorySubId = 2, CategorySubName = "Konserve ve Hazır Gıdalar", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "Çamaşır Deterjanı", Description = "Toz çamaşır deterjanı 10 kg.", UnitType = UnitType.kg, ProductNumber = "TM001", Barcode = "8680000000101", Brand = "Ariel", ImageUrl = "imageurl3", CategoryId = 2, CategoryName = "Temizlik", CategorySubId = 1, CategorySubName = "Ev Temizlik", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Sıvı Sabun", Description = "1 Litre sıvı el sabunu.", UnitType = UnitType.Litre, ProductNumber = "TM002", Barcode = "8680000000102", Brand = "Duru", ImageUrl = "imageurl4", CategoryId = 2, CategoryName = "Temizlik", CategorySubId = 3, CategorySubName = "Kişisel Hijyen", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "Şampuan", Description = "Bitkisel özlü şampuan 500ml.", UnitType = UnitType.Mililitre, ProductNumber = "KB001", Barcode = "8680000000201", Brand = "Head & Shoulders", ImageUrl = "imageurl5", CategoryId = 3, CategoryName = "Kişisel Bakım", CategorySubId = 1, CategorySubName = "Saç Bakımı", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Diş Macunu", Description = "Beyazlatıcı diş macunu 100gr.", UnitType = UnitType.Gram, ProductNumber = "KB002", Barcode = "8680000000202", Brand = "Signal", ImageUrl = "imageurl6", CategoryId = 3, CategoryName = "Kişisel Bakım", CategorySubId = 3, CategorySubName = "Ağız ve Diş Bakımı", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "Kot Pantolon", Description = "Erkek kot pantolon.", UnitType = UnitType.Adet, ProductNumber = "MD001", Barcode = "8680000000301", Brand = "Levi's", ImageUrl = "imageurl7", CategoryId = 4, CategoryName = "Moda ve Aksesuarlar", CategorySubId = 1, CategorySubName = "Giyim", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Güneş Gözlüğü", Description = "Polarize güneş gözlüğü.", UnitType = UnitType.Adet, ProductNumber = "MD002", Barcode = "8680000000302", Brand = "Ray-Ban", ImageUrl = "imageurl8", CategoryId = 4, CategoryName = "Moda ve Aksesuarlar", CategorySubId = 3, CategorySubName = "Çanta ve Aksesuarlar", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "A4 Kâğıt", Description = "500'lü fotokopi kâğıdı.", UnitType = UnitType.Paket, ProductNumber = "KO001", Barcode = "8680000000401", Brand = "Navigator", ImageUrl = "imageurl9", CategoryId = 5, CategoryName = "Kırtasiye ve Ofis", CategorySubId = 2, CategorySubName = "Kâğıt Ürünleri", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Tükenmez Kalem", Description = "10'lu tükenmez kalem seti.", UnitType = UnitType.Paket, ProductNumber = "KO002", Barcode = "8680000000402", Brand = "Faber-Castell", ImageUrl = "imageurl10", CategoryId = 5, CategoryName = "Kırtasiye ve Ofis", CategorySubId = 1, CategorySubName = "Yazı ve Çizim", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "Banyo Havlusu", Description = "Pamuklu banyo havlusu.", UnitType = UnitType.Adet, ProductNumber = "TX001", Barcode = "8680000000501", Brand = "Özdilek", ImageUrl = "imageurl11", CategoryId = 6, CategoryName = "Tekstil", CategorySubId = 3, CategorySubName = "Banyo Tekstili", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Nevresim Takımı", Description = "Çift kişilik nevresim takımı.", UnitType = UnitType.Takım, ProductNumber = "TX002", Barcode = "8680000000502", Brand = "Taç", ImageUrl = "imageurl12", CategoryId = 6, CategoryName = "Tekstil", CategorySubId = 1, CategorySubName = "Ev Tekstili", CreatedAt = DateTime.UtcNow },

                        new Product { Name = "Elektrikli Süpürge", Description = "Torbasız elektrikli süpürge.", UnitType = UnitType.Adet, ProductNumber = "EL001", Barcode = "8680000000601", Brand = "Philips", ImageUrl = "imageurl13", CategoryId = 7, CategoryName = "Elektronik", CategorySubId = 1, CategorySubName = "Ev Elektroniği", CreatedAt = DateTime.UtcNow },
                        new Product { Name = "Kablosuz Mouse", Description = "Bluetooth özellikli mouse.", UnitType = UnitType.Adet, ProductNumber = "EL002", Barcode = "8680000000602", Brand = "Logitech", ImageUrl = "imageurl14", CategoryId = 7, CategoryName = "Elektronik", CategorySubId = 3, CategorySubName = "Bilgisayar ve Aksesuarlar", CreatedAt = DateTime.UtcNow }
                    };

                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Ürün verileri başarıyla eklendi.");
                }
                else
                {
                    _logger.LogInformation("Product verileri zaten mevcut, ekleme yapılmadı.");
                }

                //Locations(Region)
                if (!await _context.Regions.AnyAsync())
                {
                    var regions = LocationsSeederCollection.GetRegions();
                    await _context.Regions.AddRangeAsync(regions);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Region verileri başarıyla eklendi.");
                }
                else
                {
                    _logger.LogInformation("Region verileri zaten mevcut, ekleme yapılmadı.");
                }

                _logger.LogInformation("Database seeding tamamlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database seeding sırasında bir hata oluştu.");
            }
        }
    }
}


