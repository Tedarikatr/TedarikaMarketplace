using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserGuidNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    AdminGuidNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByAdminId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUsers_AdminUsers_CreatedByAdminId",
                        column: x => x.CreatedByAdminId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminUsers_AdminUsers_UpdatedByAdminId",
                        column: x => x.UpdatedByAdminId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeededBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFulfilled = table.Column<bool>(type: "bit", nullable: false),
                    UserIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserGuidNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IntegrationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incoterms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incoterms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GTIPFocusArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepresentativeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepresentativePosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserGuidNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesSubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriesSubs_Categories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TaxOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BuyerAccount = table.Column<bool>(type: "bit", nullable: false),
                    SellerAccount = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BuyerUserId = table.Column<int>(type: "int", nullable: true),
                    SellerUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_BuyerUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "BuyerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_SellerUsers_SellerUserId",
                        column: x => x.SellerUserId,
                        principalTable: "SellerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GTIPCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreparationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorySubId = table.Column<int>(type: "int", nullable: true),
                    CategorySubName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CategoriesSubs_CategorySubId",
                        column: x => x.CategorySubId,
                        principalTable: "CategoriesSubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_SellerUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "SellerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreCarriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    StoreApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCarriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCarriers_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCarriers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CertificateType = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCertificates_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreCovargeLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RegionIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeighborhoodIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeighborhoodNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCovargeLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCovargeLocations_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorePaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorePaymentMethods_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    UnitTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    AllowedDomestic = table.Column<bool>(type: "bit", nullable: false),
                    AllowedInternational = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorySubId = table.Column<int>(type: "int", nullable: true),
                    CategorySubName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductRequests_CategoriesSubs_CategorySubId",
                        column: x => x.CategorySubId,
                        principalTable: "CategoriesSubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreProductRequests_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreProductRequests_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitTypes = table.Column<int>(type: "int", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    BlockedByExportBan = table.Column<bool>(type: "bit", nullable: false),
                    AllowedDomestic = table.Column<bool>(type: "bit", nullable: false),
                    AllowedInternational = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorySubName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstimatedExportCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    ProductUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LogisticsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CertificateCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlatformCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FXMarginCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoterm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimatedExportCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimatedExportCosts_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    CertificateType = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductCertificates_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductIncoterms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    IncotermId = table.Column<int>(type: "int", nullable: false),
                    EstimatedDeliveryCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductIncoterms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductIncoterms_Incoterms_IncotermId",
                        column: x => x.IncotermId,
                        principalTable: "Incoterms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProductIncoterms_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductShippingRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedDeliveryDays = table.Column<int>(type: "int", nullable: false),
                    AllowedCarriers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductShippingRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductShippingRegions_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductShowrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductShowrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductShowrooms_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StoreCertificateId = table.Column<int>(type: "int", nullable: true),
                    StoreProductCertificateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_StoreCertificates_StoreCertificateId",
                        column: x => x.StoreCertificateId,
                        principalTable: "StoreCertificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Regions_StoreProductCertificates_StoreProductCertificateId",
                        column: x => x.StoreProductCertificateId,
                        principalTable: "StoreProductCertificates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StoreCertificateId = table.Column<int>(type: "int", nullable: true),
                    StoreProductCertificateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Countries_StoreCertificates_StoreCertificateId",
                        column: x => x.StoreCertificateId,
                        principalTable: "StoreCertificates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_StoreProductCertificates_StoreProductCertificateId",
                        column: x => x.StoreProductCertificateId,
                        principalTable: "StoreProductCertificates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductExportBanneds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    GTIPCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    IsExportBanned = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExportBanneds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductExportBanneds_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductExportBanneds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provinces_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerUserId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: true),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_BuyerUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "BuyerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    BuyerCompanyId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SelectedCarrierId = table.Column<int>(type: "int", nullable: true),
                    DeliveryAddressId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_BuyerUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "BuyerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Carriers_SelectedCarrierId",
                        column: x => x.SelectedCarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Companies_BuyerCompanyId",
                        column: x => x.BuyerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "DeliveryAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreInvoices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreInvoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_CreatedByAdminId",
                table: "AdminUsers",
                column: "CreatedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UpdatedByAdminId",
                table: "AdminUsers",
                column: "UpdatedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesSubs_MainCategoryId",
                table: "CategoriesSubs",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BuyerUserId",
                table: "Companies",
                column: "BuyerUserId",
                unique: true,
                filter: "[BuyerUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyNumber",
                table: "Companies",
                column: "CompanyNumber",
                unique: true,
                filter: "[CompanyNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SellerUserId",
                table: "Companies",
                column: "SellerUserId",
                unique: true,
                filter: "[SellerUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_TaxNumber",
                table: "Companies",
                column: "TaxNumber",
                unique: true,
                filter: "[TaxNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_StoreCertificateId",
                table: "Countries",
                column: "StoreCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_StoreProductCertificateId",
                table: "Countries",
                column: "StoreProductCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_BuyerUserId_IsDefault",
                table: "DeliveryAddresses",
                columns: new[] { "BuyerUserId", "IsDefault" },
                unique: true,
                filter: "[IsDefault] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_CountryId",
                table: "DeliveryAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_DistrictId",
                table: "DeliveryAddresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_NeighborhoodId",
                table: "DeliveryAddresses",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_ProvinceId",
                table: "DeliveryAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_RegionId",
                table: "DeliveryAddresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_StateId",
                table: "DeliveryAddresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimatedExportCosts_StoreProductId",
                table: "EstimatedExportCosts",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_DistrictId",
                table: "Neighborhoods",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerCompanyId",
                table: "Orders",
                column: "BuyerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SelectedCarrierId",
                table: "Orders",
                column: "SelectedCarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExportBanneds_CountryId",
                table: "ProductExportBanneds",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExportBanneds_ProductId",
                table: "ProductExportBanneds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategorySubId",
                table: "Products",
                column: "CategorySubId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductNumber",
                table: "Products",
                column: "ProductNumber",
                unique: true,
                filter: "[ProductNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_StateId",
                table: "Provinces",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_StoreCertificateId",
                table: "Regions",
                column: "StoreCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_StoreProductCertificateId",
                table: "Regions",
                column: "StoreProductCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCarriers_CarrierId",
                table: "StoreCarriers",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCarriers_StoreId",
                table: "StoreCarriers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCertificates_StoreId",
                table: "StoreCertificates",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCovargeLocations_StoreId",
                table: "StoreCovargeLocations",
                column: "StoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreInvoices_CompanyId",
                table: "StoreInvoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInvoices_OrderId",
                table: "StoreInvoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePaymentMethods_StoreId",
                table: "StorePaymentMethods",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductCertificates_StoreProductId",
                table: "StoreProductCertificates",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductIncoterms_IncotermId",
                table: "StoreProductIncoterms",
                column: "IncotermId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductIncoterms_StoreProductId",
                table: "StoreProductIncoterms",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductRequests_CategoryId",
                table: "StoreProductRequests",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductRequests_CategorySubId",
                table: "StoreProductRequests",
                column: "CategorySubId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductRequests_StoreId",
                table: "StoreProductRequests",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ProductId",
                table: "StoreProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoreId",
                table: "StoreProducts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductShippingRegions_StoreProductId",
                table: "StoreProductShippingRegions",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductShowrooms_StoreProductId",
                table: "StoreProductShowrooms",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CompanyId",
                table: "Stores",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SellerId",
                table: "Stores",
                column: "SellerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "BuyerApplications");

            migrationBuilder.DropTable(
                name: "EstimatedExportCosts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductExportBanneds");

            migrationBuilder.DropTable(
                name: "SellerApplications");

            migrationBuilder.DropTable(
                name: "StoreCarriers");

            migrationBuilder.DropTable(
                name: "StoreCovargeLocations");

            migrationBuilder.DropTable(
                name: "StoreInvoices");

            migrationBuilder.DropTable(
                name: "StorePaymentMethods");

            migrationBuilder.DropTable(
                name: "StoreProductIncoterms");

            migrationBuilder.DropTable(
                name: "StoreProductRequests");

            migrationBuilder.DropTable(
                name: "StoreProductShippingRegions");

            migrationBuilder.DropTable(
                name: "StoreProductShowrooms");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Incoterms");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Neighborhoods");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "StoreCertificates");

            migrationBuilder.DropTable(
                name: "StoreProductCertificates");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "CategoriesSubs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BuyerUsers");

            migrationBuilder.DropTable(
                name: "SellerUsers");
        }
    }
}
