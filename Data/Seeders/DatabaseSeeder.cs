using Data.Databases;
using Entity.Auths;
using Entity.Categories;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Locations;
using Entity.Stores;
using Entity.Stores.Locations;
using Entity.Stores.Products;
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

                Store store = null;
                BuyerUser buyer = null;

                if (!await _context.SellerUsers.AnyAsync())
                {
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

                    var sellerCompany = new Company
                    {
                        Name = "Yılmaz Gıda Sanayi",
                        TaxNumber = "1234567890",
                        TaxOffice = "İstanbul Vergi Dairesi",
                        Country = "Türkiye",
                        City = "İstanbul",
                        Address = "Organize Sanayi Bölgesi",
                        Email = "info@yilmazgida.com",
                        Phone = "+902124567890",
                        CompanyNumber = "C0001",
                        Industry = "Gıda",
                        IsVerified = true,
                        IsActive = true,
                        BuyerAccount = false,
                        SellerAccount = true,
                        Type = CompanyType.Seller,
                        SellerUserId = seller.Id
                    };
                    await _context.Companies.AddAsync(sellerCompany);
                    await _context.SaveChangesAsync();

                    store = new Store
                    {
                        StoreName = "Yılmaz Toptan",
                        SellerId = seller.Id,
                        IsApproved = true,
                        IsActive = true,
                        CompanyId = sellerCompany.Id,
                        StoreProvince = "İstanbul",
                        StoreDistrict = "Beyoğlu"
                    };
                    await _context.Stores.AddAsync(store);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    store = await _context.Stores.FirstOrDefaultAsync();
                }

                if (!await _context.BuyerUsers.AnyAsync())
                {
                    buyer = new BuyerUser
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

                    var buyerCompany = new Company
                    {
                        Name = "Demir Marketler Zinciri",
                        TaxNumber = "9876543210",
                        TaxOffice = "İzmir Vergi Dairesi",
                        Country = "Türkiye",
                        City = "İzmir",
                        Address = "Alsancak No:123",
                        Email = "info@demirmarket.com",
                        Phone = "+902324567890",
                        CompanyNumber = "C0002",
                        Industry = "Perakende",
                        IsVerified = true,
                        IsActive = true,
                        BuyerAccount = true,
                        SellerAccount = false,
                        Type = CompanyType.Buyer,
                        BuyerUserId = buyer.Id
                    };
                    await _context.Companies.AddAsync(buyerCompany);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    buyer = await _context.BuyerUsers.FirstOrDefaultAsync();
                }

                //Region
                if (!await _context.Regions.AnyAsync())
                {
                    // 1. Region
                    if (!await _context.Regions.AnyAsync())
                    {
                        var regions = LocationsSeederCollection.GetRegions();
                        await _context.Regions.AddRangeAsync(regions);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Region verileri başarıyla eklendi.");
                    }
                    else _logger.LogInformation("Region verileri zaten mevcut, ekleme yapılmadı.");

                    // 2. Avrupa -> Country
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
                        _logger.LogWarning("EU kodlu Region bulunamadı, ülke verileri eklenemedi.");
                    else
                        _logger.LogInformation("Avrupa ülkeleri zaten eklenmiş, tekrar eklenmedi.");

                    // 3. Türkiye -> Province
                    var turkeys = await _context.Countries.FirstOrDefaultAsync(c => c.Code == "TR");
                    if (turkeys != null)
                    {
                        var existingProvinces = await _context.Provinces
                            .Where(p => p.CountryId == turkeys.Id)
                            .Select(p => p.Name)
                            .ToListAsync();

                        var newProvinces = LocationsSeederCollection.GetTurkeyProvinceCollection(turkeys.Id)
                            .Where(p => !existingProvinces.Contains(p.Name))
                            .ToList();

                        if (newProvinces.Any())
                        {
                            foreach (var province in newProvinces)
                                province.CountryId = turkeys.Id;

                            await _context.Provinces.AddRangeAsync(newProvinces);
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Türkiye illeri başarıyla eklendi.");
                        }
                        else
                            _logger.LogInformation("Türkiye illeri zaten mevcut, eklenmedi.");

                        // İzmir -> District
                        var izmirProvince = await _context.Provinces
                            .Include(p => p.Country)
                            .FirstOrDefaultAsync(p => p.Country.Code == "TR" && p.Name == "İzmir");

                        if (izmirProvince != null)
                        {
                            if (!await _context.Districts.AnyAsync(d => d.ProvinceId == izmirProvince.Id))
                            {
                                var izmirDistricts = LocationsSeederCollection.GetIzmirDistricts(izmirProvince.Id);
                                await _context.Districts.AddRangeAsync(izmirDistricts);
                                await _context.SaveChangesAsync();
                                _logger.LogInformation("İzmir'e ait ilçeler başarıyla eklendi.");
                            }
                            else
                                _logger.LogInformation("İzmir ilçeleri zaten mevcut, tekrar eklenmedi.");
                        }
                        else
                            _logger.LogWarning("İzmir ili bulunamadı. Lütfen Province seed işlemini kontrol edin.");
                    }

                    // 4. Fransa -> Province + District
                    var frances = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Code == "FR");
                    if (frances != null)
                    {
                        if (!await _context.Provinces.AnyAsync(p => p.CountryId == frances.Id))
                        {
                            var franceProvinces = LocationsSeederCollection.GetFranceProvinces(frances.Id);
                            _context.ChangeTracker.Clear();
                            await _context.Provinces.AddRangeAsync(franceProvinces);
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Fransa şehirleri başarıyla eklendi.");
                        }
                        else
                            _logger.LogInformation("Fransa şehirleri zaten mevcut, tekrar eklenmedi.");

                        if (!await _context.Districts.AnyAsync(d => d.Province.Country.Code == "FR"))
                        {
                            var franceProvincesList = await _context.Provinces
                                .Where(p => p.Country.Code == "FR")
                                .ToListAsync();

                            if (franceProvincesList.Any())
                            {
                                var districts = LocationsSeederCollection.GetFrenchDistricts();
                                var random = new Random();
                                foreach (var district in districts)
                                {
                                    var randomProvince = franceProvincesList[random.Next(franceProvincesList.Count)];
                                    district.ProvinceId = randomProvince.Id;
                                }
                                await _context.Districts.AddRangeAsync(districts);
                                await _context.SaveChangesAsync();
                                _logger.LogInformation("Fransa'ya ait ilçeler başarıyla eklendi.");
                            }
                            else
                                _logger.LogWarning("Fransa şehirleri bulunamadı, önce Province seed işlemini kontrol edin.");
                        }
                        else
                            _logger.LogInformation("Fransa ilçeleri zaten mevcut.");
                    }
                    else _logger.LogWarning("Fransa (FR) ülkesi bulunamadı, şehir/district verileri eklenemedi.");

                    // 5. Almanya -> State + Province
                    var germanys = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Code == "DE");
                    if (germanys != null)
                    {
                        if (!await _context.States.AnyAsync(s => s.CountryId == germanys.Id))
                        {
                            var germanStates = LocationsSeederCollection.GetGermanStates(germanys.Id);
                            _context.ChangeTracker.Clear();
                            await _context.States.AddRangeAsync(germanStates);
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Almanya eyaletleri başarıyla eklendi.");
                        }
                        else
                            _logger.LogInformation("Almanya eyalet verileri zaten mevcut, tekrar eklenmedi.");

                        if (await _context.States.AnyAsync(s => s.CountryId == germanys.Id) &&
                            !await _context.Provinces.AnyAsync(p => p.CountryId == germanys.Id))
                        {
                            var germanStatesDict = await _context.States
                                .Where(s => s.CountryId == germanys.Id)
                                .ToDictionaryAsync(s => s.Name, s => s.Id);

                            if (!germanStatesDict.Any())
                            {
                                _logger.LogWarning("Almanya eyaletleri bulunamadı!");
                            }
                            else
                            {
                                var germanProvinces = LocationsSeederCollection.GetGermanProvinces(germanStatesDict);
                                foreach (var province in germanProvinces)
                                    province.CountryId = germanys.Id;

                                _context.ChangeTracker.Clear();
                                await _context.Provinces.AddRangeAsync(germanProvinces);
                                await _context.SaveChangesAsync();
                                _logger.LogInformation("Almanya şehirleri başarıyla eklendi.");
                            }
                        }
                    }


                    // Buyer için hiyerarşik teslimat adresi (EU > TR > İzmir > Konak > Alsancak örneği)

                    var regionEu = await _context.Regions.FirstOrDefaultAsync(r => r.Code == "EU");
                    if (regionEu != null)
                    {
                        var countryTr = await _context.Countries.FirstOrDefaultAsync(c => c.RegionId == regionEu.Id && c.Code == "TR");
                        if (countryTr != null)
                        {
                            var provinceIzmir = await _context.Provinces.FirstOrDefaultAsync(p => p.CountryId == countryTr.Id && p.Name == "İzmir");
                            if (provinceIzmir != null)
                            {
                                var districtKonak = await _context.Districts.FirstOrDefaultAsync(d => d.ProvinceId == provinceIzmir.Id && d.Name == "Konak");
                                if (districtKonak != null)
                                {
                                    var neighborhoodAlsancak = await _context.Neighborhoods
                                        .FirstOrDefaultAsync(n => n.DistrictId == districtKonak.Id && n.Name.Contains("Alsancak")); // isim eşleşmesi değişebilir

                                    var deliveryAddress = new DeliveryAddress
                                    {
                                        BuyerUserId = buyer.Id,
                                        CountryId = countryTr.Id,
                                        StateId = null, // Türkiye'de genelde kullanılmaz
                                        ProvinceId = provinceIzmir.Id,
                                        DistrictId = districtKonak.Id,
                                        NeighborhoodId = neighborhoodAlsancak?.Id,
                                        AddressLine = "Atatürk Caddesi No:45, Alsancak",
                                        PostalCode = "35220",
                                        IsDefault = true
                                    };

                                    await _context.DeliveryAddresses.AddAsync(deliveryAddress);
                                    await _context.SaveChangesAsync();
                                    _logger.LogInformation("Buyer için teslimat adresi başarıyla eklendi: İzmir / Konak / Alsancak.");
                                }
                                else _logger.LogWarning("Konak ilçesi bulunamadı.");
                            }
                            else _logger.LogWarning("İzmir ili bulunamadı.");
                        }
                        else _logger.LogWarning("Türkiye ülkesi bulunamadı.");
                    }
                    else
                    {
                        _logger.LogWarning("EU bölgesi bulunamadı.");
                    }


                }

                // Admin
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
                }
                else
                {
                    _logger.LogInformation("SuperAdmin zaten mevcut, ekleme yapılmadı.");
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

                // Products
                if (!await _context.Products.AnyAsync())
                {
                    var existingBarcodes = new HashSet<string>(await _context.Products.Select(p => p.Barcode).ToListAsync());
                    var existingProductNumbers = new HashSet<string>(await _context.Products.Select(p => p.ProductNumber).ToListAsync());

                    var random = new Random();
                    var counter = 1;

                    // Tüm ürünleri al
                    var allProducts = ProductSeederCollection.GetProductList()
                        .Select(p =>
                        {
                            // Unique Barcode
                            string newBarcode;
                            do
                            {
                                newBarcode = "868" + random.Next(100000000, 999999999).ToString();
                            } while (existingBarcodes.Contains(newBarcode));
                            p.Barcode = newBarcode;
                            existingBarcodes.Add(newBarcode);

                            // Unique ProductNumber
                            string newProductNumber;
                            do
                            {
                                newProductNumber = $"GD{counter++.ToString("D4")}";
                            } while (existingProductNumbers.Contains(newProductNumber));
                            p.ProductNumber = newProductNumber;
                            existingProductNumbers.Add(newProductNumber);

                            p.CreatedAt = DateTime.UtcNow;
                            return p;
                        })
                        .ToList();

                    await _context.Products.AddRangeAsync(allProducts);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("{Count} adet ürün başarıyla eklendi.", allProducts.Count);

                    // Varsayılan bir Store varsa 40 ürün için StoreProduct oluştur
                    var firstStore = await _context.Stores.FirstOrDefaultAsync();
                    if (firstStore != null)
                    {
                        var selectedProducts = allProducts.Take(40).ToList();

                        var storeProducts = selectedProducts.Select(p => new StoreProduct
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Brand = p.Brand,
                            ProductNumber = p.ProductNumber,
                            UnitTypes = (int)p.UnitType,
                            UnitType = p.UnitType,
                            ProductId = p.Id,
                            StoreId = firstStore.Id,
                            UnitPrice = random.Next(10, 100),
                            StockQuantity = 100,
                            MinOrderQuantity = 1,
                            MaxOrderQuantity = 50,
                            IsActive = true,
                            IsOnSale = true,
                            AllowedDomestic = true,
                            AllowedInternational = false,
                            ImageUrl = p.ImageUrl,
                            StoreProductImageUrl = p.ImageUrl,
                            CategoryName = p.CategoryName,
                            CategorySubName = p.CategorySubName
                        }).ToList();

                        await _context.StoreProducts.AddRangeAsync(storeProducts);
                        await _context.SaveChangesAsync();

                        _logger.LogInformation("Store'a bağlı 40 adet StoreProduct başarıyla eklendi.");
                    }
                    else
                    {
                        _logger.LogWarning("Store bulunamadı, StoreProduct eklenemedi.");
                    }
                }
                
                else
                {
                    _logger.LogInformation("Seller and/or buyer data already exists, skipping creation.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database seeding sırasında bir hata oluştu.");
            }
        }
    }
}


