using Data.Databases;
using Entity.Auths;
using Entity.Categories;
using Entity.Companies;
using Entity.Markets.Locations;
using Entity.Products;
using Entity.Stores.Products;
using Entity.Stores;
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

                // SuperAdmin
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

                // StandartAdmin
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

                //BuyerUser
                if (!await _context.BuyerUsers.AnyAsync())
                {
                    var buyer = new BuyerUser
                    {
                        Name = "Ayşe",
                        LastName = "Demir",
                        Email = "ayse@tedarika.com",
                        Phone = "+905559998877",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        UserNumber = "B1001",
                        UserGuidNumber = Guid.NewGuid(),
                        Status = true,
                        UserType = UserType.Buyer
                    };

                    await _context.BuyerUsers.AddAsync(buyer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("BuyerUser verileri zaten mevcut, ekleme yapılmadı.");
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
                    var products = ProductSeederCollection.GetProductList();

                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Ürün verileri başarıyla eklendi.");
                }
                else
                {
                    _logger.LogInformation("Product verileri zaten mevcut, ekleme yapılmadı.");
                }

                //SellerUser
                if (!await _context.SellerUsers.AnyAsync())
                {
                    // 1️⃣ Seller User
                    var seller = new SellerUser
                    {
                        Name = "Ali",
                        LastName = "Yılmaz",
                        Email = "ali@tedarika.com",
                        Phone = "+905551112233",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                        UserNumber = "S1001",
                        UserGuidNumber = Guid.NewGuid(),
                        Status = true,
                        UserType = UserType.Seller
                    };
                    await _context.SellerUsers.AddAsync(seller);
                    await _context.SaveChangesAsync();

                    // 2️⃣ Company
                    var company = new Company
                    {
                        Name = "Yılmaz Gıda Sanayi",
                        TaxNumber = "1234567890",
                        TaxOffice = "İstanbul Vergi Dairesi",
                        Country = "Türkiye",
                        City = "İstanbul",
                        Address = "Organize Sanayi Bölgesi",
                        Email = "info@yilmazgida.com",
                        Phone = "+902124567890",
                        CompanyNumber = "C2024",
                        Industry = "Gıda",
                        IsVerified = true,
                        IsActive = true,
                        BuyerAccount = false,
                        SellerAccount = true,
                        Type = CompanyType.Seller,
                        SellerUserId = seller.Id
                    };
                    await _context.Companies.AddAsync(company);
                    await _context.SaveChangesAsync();

                    // 3️⃣ Store
                    var store = new Store
                    {
                        StoreName = "Yılmaz Toptan",
                        SellerId = seller.Id,
                        IsApproved = true,
                        IsActive = true,
                        CompanyId = company.Id,
                        StoreProvince = "İstanbul",
                        StoreDistrict = "Beyoğlu"
                    };
                    await _context.Stores.AddAsync(store);
                    await _context.SaveChangesAsync();

                    // 4️⃣ Ana Ürün
                    var product = new Product
                    {
                        Name = "Kuru Nohut",
                        Description = "İri taneli, doğal kuru nohut.",
                        Brand = "Tedarika Agro",
                        UnitTypes = "Kg",
                        UnitType = UnitType.kg,
                        ProductNumber = "P1001",
                        Barcode = "8690000000011",
                        ImageUrl = "https://cdn.tedarika.com/images/kuru-nohut.jpg",
                        CreatedAt = DateTime.UtcNow
                    };
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                    // 5️⃣ StoreProduct bağlantısı
                    var storeProduct = new StoreProduct
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Brand = product.Brand,
                        UnitTypes = (int)UnitType.kg,
                        UnitType = UnitType.kg,
                        ProductId = product.Id,
                        StoreId = store.Id,
                        Price = 45.00m,
                        StockQuantity = 500,
                        MinOrderQuantity = 10,
                        MaxOrderQuantity = 100,
                        IsActive = true,
                        IsOnSale = true,
                        AllowedDomestic = true,
                        AllowedInternational = true,
                        ImageUrl = product.ImageUrl,
                        StoreImageUrl = product.ImageUrl,
                        CategoryName = "Bakliyat",
                        CategorySubName = "Nohut"
                    };
                    await _context.StoreProducts.AddAsync(storeProduct);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("SellerUser verileri zaten mevcut, ekleme yapılmadı.");
                }

                //Region
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


                // Avrupa Country 
                var europeanRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Code == "EU");

                if (europeanRegion != null && !await _context.Countries.AnyAsync(c => c.RegionId == europeanRegion.Id))
                {
                    var europeanCountries = LocationsSeederCollection.GetEuropeanCountries(europeanRegion.Id);
                    _context.ChangeTracker.Clear();
                    await _context.Countries.AddRangeAsync(europeanCountries);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Avrupa bölgesine ait ülkeler başarıyla eklendi.");
                }
                else if (europeanRegion == null)
                {
                    _logger.LogWarning("EU kodlu Region bulunamadı, ülke verileri eklenemedi.");
                }
                else
                {
                    _logger.LogInformation("Avrupa ülkeleri zaten eklenmiş, tekrar eklenmedi.");
                }

                // Turkiye (Province)
                var turkey = await _context.Countries.FirstOrDefaultAsync(c => c.Code == "TR");
                if (turkey != null)
                {
                    var existingProvinces = await _context.Provinces
                        .Where(p => p.CountryId == turkey.Id)
                        .Select(p => p.Name)
                        .ToListAsync();

                    var newProvinces = LocationsSeederCollection.GetTurkeyProvinceCollection(turkey.Id)
                        .Where(p => !existingProvinces.Contains(p.Name))
                        .ToList();

                    if (newProvinces.Any())
                    {
                        foreach (var province in newProvinces)
                        {
                            province.CountryId = turkey.Id;
                        }

                        await _context.Provinces.AddRangeAsync(newProvinces);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Türkiye illeri başarıyla eklendi.");
                    }
                    else
                    {
                        _logger.LogInformation("Türkiye illeri zaten mevcut, eklenmedi.");
                    }
                }

                //Türkiye İzmir
                if (turkey != null)
                {
                    var izmirProvince = await _context.Provinces
                        .Include(p => p.Country)
                        .FirstOrDefaultAsync(p => p.Country.Code == "TR" && p.Name == "İzmir");

                    if (izmirProvince != null)
                    {
                        bool anyDistrictExists = await _context.Districts.AnyAsync(d => d.ProvinceId == izmirProvince.Id);

                        if (!anyDistrictExists)
                        {
                            var izmirDistricts = LocationsSeederCollection.GetIzmirDistricts(izmirProvince.Id);
                            await _context.Districts.AddRangeAsync(izmirDistricts);
                            await _context.SaveChangesAsync();

                            _logger.LogInformation("İzmir'e ait ilçeler başarıyla eklendi.");
                        }
                        else
                        {
                            _logger.LogInformation("İzmir ilçeleri zaten mevcut, tekrar eklenmedi.");
                        }
                    }
                    else
                    {
                        _logger.LogWarning("İzmir ili bulunamadı. Lütfen Province seed işlemini kontrol edin.");
                    }
                }

                // Fransa (Province)
                var france = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Code == "FR");

                if (france != null && !await _context.Provinces.AnyAsync(p => p.CountryId == france.Id))
                {
                    var franceProvinces = LocationsSeederCollection.GetFranceProvinces(france.Id);
                    _context.ChangeTracker.Clear();
                    await _context.Provinces.AddRangeAsync(franceProvinces);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Fransa şehirleri başarıyla eklendi.");
                }
                else if (france == null)
                {
                    _logger.LogWarning("Fransa (FR) ülkesi bulunamadı, şehirler eklenemedi.");
                }
                else
                {
                    _logger.LogInformation("Fransa şehirleri zaten mevcut, tekrar eklenmedi.");
                }

                // Fransa ilçeleri (Districts) ekleniyor
                if (!await _context.Districts.AnyAsync(d => d.Province.Country.Code == "FR"))
                {
                    var franceProvinces = await _context.Provinces
                        .Where(p => p.Country.Code == "FR")
                        .ToListAsync();

                    if (!franceProvinces.Any())
                    {
                        _logger.LogWarning("Fransa şehirleri bulunamadı, önce Province seed işlemini kontrol edin.");
                    }
                    else
                    {
                        var districts = LocationsSeederCollection.GetFrenchDistricts();
                        var random = new Random();

                        // Her district'e rastgele bir province atama (örnek amaçlı, daha iyi eşleme yapılabilir)
                        for (int i = 0; i < districts.Count; i++)
                        {
                            var randomProvince = franceProvinces[random.Next(franceProvinces.Count)];
                            districts[i].ProvinceId = randomProvince.Id;
                        }

                        await _context.Districts.AddRangeAsync(districts);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Fransa'ya ait ilçeler (Districts) başarıyla eklendi.");
                    }
                }
                else
                {
                    _logger.LogInformation("Fransa'ya ait ilçeler (Districts) zaten mevcut.");
                }


                // Almanya eyaletleri (State) verisini ekleme
                var germany = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Code == "DE");

                if (germany != null && !await _context.States.AnyAsync(s => s.CountryId == germany.Id))
                {
                    var germanStates = LocationsSeederCollection.GetGermanStates(germany.Id);
                    _context.ChangeTracker.Clear();
                    await _context.States.AddRangeAsync(germanStates);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Almanya eyaletleri başarıyla eklendi.");
                }
                else if (germany == null)
                {
                    _logger.LogWarning("Almanya (DE) ülkesi bulunamadı, eyalet verileri eklenemedi.");
                }
                else
                {
                    _logger.LogInformation("Almanya eyalet verileri zaten mevcut, tekrar eklenmedi.");
                }

                // Almanya şehirleri (Province) verisini ekleme
                if (germany != null &&
                    await _context.States.AnyAsync(s => s.CountryId == germany.Id) &&
                    !await _context.Provinces.AnyAsync(p => p.CountryId == germany.Id))
                {
                    // Eyaletleri dictionary olarak al
                    var germanStates = await _context.States
                        .Where(s => s.CountryId == germany.Id)
                        .ToDictionaryAsync(s => s.Name, s => s.Id);

                    if (!germanStates.Any())
                    {
                        _logger.LogWarning("Almanya eyaletleri bulunamadı!");
                        return;
                    }

                    var germanProvinces = LocationsSeederCollection.GetGermanProvinces(germanStates);

                    // CountryId'leri manuel olarak ayarla
                    foreach (var province in germanProvinces)
                    {
                        province.CountryId = germany.Id;
                    }

                    _context.ChangeTracker.Clear();
                    await _context.Provinces.AddRangeAsync(germanProvinces);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Almanya şehirleri (Province) başarıyla eklendi.");
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


