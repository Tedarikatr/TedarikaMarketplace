using API.Filter;
using API.Mappings;
using AutoMapper;
using Data.Databases;
using Data.Seeders;
using FluentValidation;
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
using static API.Validators.Auth.AuthValidator;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

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
builder.Services.AddScoped<IAdminUserService, AdminUserService>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IBuyerUserService, BuyerUserService>();
builder.Services.AddScoped<IBuyerUserRepository, BuyerUserRepository>();
builder.Services.AddScoped<ISellerUserService, SellerUserService>();
builder.Services.AddScoped<ISellerUserRepository, SellerUserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// **FluentValidation Eklenmesi**
builder.Services.AddValidatorsFromAssemblyContaining<BuyerUserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BuyerLoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SellerRegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SellerLoginValidator>();

// **MediatR Kullanýmý**
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// **HttpContextAccessor Eklenmesi**
builder.Services.AddHttpContextAccessor();

// **SignalR Eklenmesi**
builder.Services.AddSignalR();

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

// **Swagger Konfigurasyonu (Seller, Buyer, Admin Ayrýmý)**
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());


    c.OperationFilter<EnumOperationFilter>();

    c.SwaggerDoc("seller", new OpenApiInfo { Title = "Seller API", Version = "v1" });
    c.SwaggerDoc("buyer", new OpenApiInfo { Title = "Buyer API", Version = "v1" });
    c.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });

    //c.EnableAnnotations();

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
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
    //.AddNewtonsoftJson(options =>
    //{
    //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    //    options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
    //}); 

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// **Veritabaný Baþlangýç Verileri (Seeder)**
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

// **Swagger UI Route Güncellemesi**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/seller/swagger.json", "Seller API V1");
        c.SwaggerEndpoint("/swagger/buyer/swagger.json", "Buyer API V1");
        c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API V1");
        c.RoutePrefix = "swagger";
    });
}

// **Middleware'ler**
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
