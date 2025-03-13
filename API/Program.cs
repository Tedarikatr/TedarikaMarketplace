using API.Mappings;
using AutoMapper;
using Data.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Auth.IRepositorys;
using Repository.Auth.Repositorys;
using Serilog;
using Services.Auth.Helper;
using Services.Auth.IServices;
using Services.Auth.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ?? **Database Baðlantýsý**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// ?? **Dependency Injection - Repository & Services**
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IBuyerUserService, BuyerUserService>();
builder.Services.AddScoped<IBuyerUserRepository, BuyerUserRepository>();
builder.Services.AddScoped<ISellerUserService, SellerUserService>();
builder.Services.AddScoped<ISellerUserRepository, SellerUserRepository>();

// AutoMapper Konfigürasyonu
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>(); // Mapping profili ekleniyor
});

try
{
    config.AssertConfigurationIsValid(); // Konfigürasyon hatalarýný kontrol et
}
catch (Exception ex)
{
    Log.Error(ex, "AutoMapper konfigurasyon hatasý");
    Console.WriteLine("AutoMapper konfigurasyon hatasý:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}
// ?? **AutoMapper Entegrasyonu**
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton(config.CreateMapper()); // AutoMapper'ý servislere ekleyelim.


// ?? **Swagger Konfigurasyonu (Seller, Buyer, Admin Ayrýmý)**
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("seller", new OpenApiInfo { Title = "Seller API", Version = "v1" });
    c.SwaggerDoc("buyer", new OpenApiInfo { Title = "Buyer API", Version = "v1" });
    c.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Bearer token ile giriþ yapýn. 'Bearer {your token}' formatýnda giriniz.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new string[] {}
        }
    });
});

// ?? **CORS Ayarlarý**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ?? **JWT Authentication Konfigurasyonu**
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"]
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ?? **Hata Yönetimi**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/seller/swagger.json", "Seller API V1");
        c.SwaggerEndpoint("/swagger/buyer/swagger.json", "Buyer API V1");
        c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API V1");
    });
}

// ?? **Middleware'ler**
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
