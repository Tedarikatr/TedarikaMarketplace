using API.Filter;
using API.Helpers;
using API.Mappings;
using API.Validators.Stores.StoreCoverageValidator;
using AutoMapper;
using Data.Databases;
using Data.Seeders;
using Domain.Companies.Events;
using Domain.Orders.Services;
using Domain.Products.Events;
using Domain.Stores.Events;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Auths.IRepositorys;
using Repository.Auths.Repositorys;
using Repository.Baskets.IRepositorys;
using Repository.Baskets.Repositorys;
using Repository.Carriers.IRepositorys;
using Repository.Carriers.Repositorys;
using Repository.Categories.IRepositorys;
using Repository.Categories.Repositorys;
using Repository.Companys.IRepositorys;
using Repository.Companys.Repositorys;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.DeliveryAddresses.Repositorys;
using Repository.Forms.IRepositorys;
using Repository.Forms.Repositorys;
using Repository.Locations.IRepositorys;
using Repository.Locations.Repositorys;
using Repository.Orders.IRepositorys;
using Repository.Orders.Repositorys;
using Repository.Payments.IRepositorys;
using Repository.Payments.Repositorys;
using Repository.Product.IRepositorys;
using Repository.Product.Repositorys;
using Repository.Stores;
using Repository.Stores.Locations.IRepositorys;
using Repository.Stores.Locations.Repositorys;
using Repository.Stores.Product.IRepositorys;
using Repository.Stores.Product.Repositorys;
using Serilog;
using Services.Auths.Helper;
using Services.Auths.IServices;
using Services.Auths.Services;
using Services.Availability.IServices;
using Services.Availability.Services;
using Services.Baskets.IServices;
using Services.Baskets.Services;
using Services.Carriers.IServices;
using Services.Carriers.Services;
using Services.Categories.IServices;
using Services.Categories.Services;
using Services.Companys.IServices;
using Services.Companys.Services;
using Services.DeliveryAddress.IService;
using Services.DeliveryAddress.Services;
using Services.Files.IServices;
using Services.Files.Services;
using Services.Forms.IServices;
using Services.Forms.Services;
using Services.Locations.IServices;
using Services.Locations.Services;
using Services.Notification.HelperService;
using Services.Notification.HelperService.MailTemplates;
using Services.Notification.IServices;
using Services.Notification.Service;
using Services.Orders.IServices;
using Services.Orders.Service;
using Services.Payments.IServices;
using Services.Payments.Services;
using Services.Product.IServices;
using Services.Product.Services;
using Services.Stores;
using Services.Stores.Locations.IServices;
using Services.Stores.Locations.Services;
using Services.Stores.Product.IServices;
using Services.Stores.Product.Services;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using static API.Validators.Auth.AuthValidator;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),                  
        typeof(CompanyCreatedEvent).Assembly,                
        typeof(ProductCategoryUpdatedEvent).Assembly,         
        typeof(StoreCreatedEvent).Assembly
    );
});

// **AutoMapper Konfigürasyonu**
builder.Services.AddAutoMapper(typeof(MappingProfile));

try
{
    var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
    config.AssertConfigurationIsValid();
}
catch (Exception ex)
{
    Log.Error(ex, "AutoMapper konfigurasyon hatasý");
    Console.WriteLine("AutoMapper konfigurasyon hatasý:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    throw;
}

// **MemoryCache ve Distributed Cache Eklenmesi**
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

// **Environment ve JSON Config Eklenmesi**
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// **Database Baðlantýsý**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// **Dependency Injection - Repository & Services**
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<DatabaseSeeder>();


//Auth
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IBuyerUserService, BuyerUserService>();
builder.Services.AddScoped<IBuyerUserRepository, BuyerUserRepository>();
builder.Services.AddScoped<ISellerUserService, SellerUserService>();
builder.Services.AddScoped<ISellerUserRepository, SellerUserRepository>();

builder.Services.AddScoped<AdminUserContextHelper>();
builder.Services.AddScoped<BuyerUserContextHelper>();
builder.Services.AddScoped<SellerUserContextHelper>();

//Availability
builder.Services.AddScoped<IStoreAvailabilityService, StoreAvailabilityService>();

//Baskets
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();

//Carriers
builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();
builder.Services.AddScoped<ICarrierService, CarrierService>();
builder.Services.AddScoped<ICarrierWebhookService, CarrierWebhookService>();

//Categories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategorySubRepository, CategorySubRepository>();
builder.Services.AddScoped<ICategorySubService, CategorySubService>();

//Company
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

//DeliveryAddress
builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();

//Forms
builder.Services.AddScoped<ISellerApplicationRepository, SellerApplicationRepository>();
builder.Services.AddScoped<ISellerApplicationService, SellerApplicationService>();

//Files
builder.Services.AddScoped<IFilesService, AzureBlobService>();
builder.Services.AddScoped<IPdfService, AzureBlobPdfService>();

//Markets
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();

//Notifications
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ISmsSender, SmsSender>();
builder.Services.AddScoped<IPushSender, PushSender>();
builder.Services.AddScoped<IWebSocketSender, WebSocketSender>();

//Ordes
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

//Payments
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Products
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

//Stores
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();

builder.Services.AddScoped<IStoreCoverageRepository, StoreCoverageRepository>();
builder.Services.AddScoped<IStoreCoverageService, StoreCoverageService>();

builder.Services.AddScoped<IStoreProductCertificateRepository, StoreProductCertificateRepository>();
builder.Services.AddScoped<IStoreProductCertificateService, StoreProductCertificateService>();

builder.Services.AddScoped<IStoreProductIncotermRepository, StoreProductIncotermRepository>();
builder.Services.AddScoped<IStoreProductIncotermService, StoreProductIncotermService>();

builder.Services.AddScoped<IStoreProductRepository, StoreProductRepository>();
builder.Services.AddScoped<IStoreProductService, StoreProductService>();

builder.Services.AddScoped<IStoreProductRequestRepository, StoreProductRequestRepository>();
builder.Services.AddScoped<IStoreProductRequestService, StoreProductRequestService>();

builder.Services.AddScoped<IStoreProductShippingRegionRepository, StoreProductShippingRegionRepository>();
builder.Services.AddScoped<IStoreProductShippingRegionService, StoreProductShippingRegionService>();

builder.Services.AddScoped<IStoreProductShowroomRepository, StoreProductShowroomRepository>();
builder.Services.AddScoped<IStoreProductShowroomService, StoreProductShowroomService>();

// **FluentValidation Eklenmesi**
// **FluentValidation Eklenmesi**
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<BuyerUserCreateValidator>()
    .AddValidatorsFromAssemblyContaining<BuyerLoginValidator>()
    .AddValidatorsFromAssemblyContaining<SellerRegisterValidator>()
    .AddValidatorsFromAssemblyContaining<SellerLoginValidator>()
    .AddValidatorsFromAssemblyContaining<StoreCoverageValidator>();


// **HttpContextAccessor Eklenmesi**
builder.Services.AddHttpContextAccessor();

// **SignalR Eklenmesi**
builder.Services.AddSignalR();

// **Swagger Konfigurasyonu (Seller, Buyer, Admin Ayrýmý)**
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tedarika MarketPlace API", Version = "v1" });

    c.OperationFilter<SwaggerFileUploadOperationFilter>();

    c.SwaggerDoc("seller", new OpenApiInfo { Title = "Seller API", Version = "v1" });
    c.SwaggerDoc("buyer", new OpenApiInfo { Title = "Buyer API", Version = "v1" });
    c.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });


    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>()
        }
    });
});

// **CORS Politikasý Güncellemesi**
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// **JWT Authentication Konfigurasyonu**
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// **Veritabaný Baþlangýç Verileri (Seeder)**
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TedarikaMarketplace API v1");
    c.SwaggerEndpoint("/swagger/seller/swagger.json", "Seller API V1");
    c.SwaggerEndpoint("/swagger/buyer/swagger.json", "Buyer API V1");
    c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API V1");
    c.RoutePrefix = "swagger";
});

// **Middleware'ler**
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
