using API.Mappings;
using AutoMapper;
using Data.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Auths.IRepositorys;
using Repository.Auths.Repositorys;
using Repository.Companys.IRepositorys;
using Repository.Companys.Repositorys;
using Serilog;
using Services.Auths.Helper;
using Services.Auths.IServices;
using Services.Auths.Services;
using Services.Companys.IServices;
using Services.Companys.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// ?? **Database Ba�lant�s�**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// ?? **Dependency Injection - Repository & Services**
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IBuyerUserService, BuyerUserService>();
builder.Services.AddScoped<IBuyerUserRepository, BuyerUserRepository>();
builder.Services.AddScoped<ISellerUserService, SellerUserService>();
builder.Services.AddScoped<ISellerUserRepository, SellerUserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// **AutoMapper Konfig�rasyonu**
builder.Services.AddAutoMapper(typeof(MappingProfile)); 

try
{
    var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
    config.AssertConfigurationIsValid();
}
catch (Exception ex)
{
    Log.Error(ex, "AutoMapper konfigurasyon hatas�");
    Console.WriteLine("AutoMapper konfigurasyon hatas�:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    throw;
}

// ?? **Swagger Konfigurasyonu (Seller, Buyer, Admin Ayr�m�)**
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("seller", new OpenApiInfo { Title = "Seller API", Version = "v1" });
    c.SwaggerDoc("buyer", new OpenApiInfo { Title = "Buyer API", Version = "v1" });
    c.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Bearer token ile giri� yap�n. 'Bearer {your token}' format�nda giriniz.",
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

// ?? **CORS Ayarlar�**
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

// ?? **Hata Y�netimi**
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
