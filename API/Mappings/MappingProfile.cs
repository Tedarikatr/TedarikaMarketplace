using AutoMapper;
using Data.Dtos.Auths;
using Data.Dtos.Availability;
using Data.Dtos.Baskets;
using Data.Dtos.Carriers;
using Data.Dtos.Categories;
using Data.Dtos.Companies;
using Data.Dtos.DeliveryAddresses;
using Data.Dtos.Locations;
using Data.Dtos.Payments;
using Data.Dtos.Product;
using Data.Dtos.Stores;
using Data.Dtos.Stores.Carriers;
using Data.Dtos.Stores.Markets;
using Data.Dtos.Stores.Products;
using Entity.Auths;
using Entity.Baskets;
using Entity.Carriers;
using Entity.Categories;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Locations;
using Entity.Payments;
using Entity.Products;
using Entity.Stores;
using Entity.Stores.Carriers;
using Entity.Stores.Locations;
using Entity.Stores.Products;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AuthMappings.RegisterMappings(this);
            CompanyMappings.RegisterMappings(this);
            CarrierMappings.RegisterMappings(this);
            CategoryMappings.RegisterMappings(this);
            StoreMappings.RegisterMappings(this);
            ProductMappings.RegisterMappings(this);
            DeliveryAddressMappings.RegisterMappings(this);
            LocationMappings.RegisterMappings(this);
            MarketMappings.RegisterMappings(this);
            StoreProductRequestMappings.RegisterMappings(this);
            BasketMappings.RegisterMappings(this);
            PaymentMappings.RegisterMappings(this);
            AvailabilityMappings.RegisterMappings(this);
            StoreCarrierMappings.RegisterMappings(this);
        }

        #region 1️⃣ AUTH Mappings
        private static class AuthMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<BuyerUser, BuyerUserDto>().ReverseMap();

                profile.CreateMap<BuyerUserCreateDto, BuyerUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.MapFrom(src => Guid.NewGuid()))
                    .ForMember(dest => dest.UserNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => true))
                    .ForMember(dest => dest.UserType, opt => opt.MapFrom(_ => UserType.Buyer))
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<BuyerLoginDto, BuyerUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.Ignore())
                    .ForMember(dest => dest.LastName, opt => opt.Ignore())
                    .ForMember(dest => dest.Phone, opt => opt.Ignore())
                    .ForMember(dest => dest.Email, opt => opt.Ignore())
                    .ForMember(dest => dest.UserNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.UserType, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore());

                profile.CreateMap<SellerUser, SellerUserDto>().ReverseMap();

                profile.CreateMap<SellerRegisterDto, SellerUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.MapFrom(src => Guid.NewGuid()))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => true))
                    .ForMember(dest => dest.UserType, opt => opt.MapFrom(_ => UserType.Seller))
                    .ForMember(dest => dest.UserNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<SellerLoginDto, SellerUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.Ignore())
                    .ForMember(dest => dest.LastName, opt => opt.Ignore())
                    .ForMember(dest => dest.Email, opt => opt.Ignore())
                    .ForMember(dest => dest.Phone, opt => opt.Ignore())
                    .ForMember(dest => dest.UserNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.UserType, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                     .ForMember(dest => dest.StoreId, opt => opt.Ignore())  
                    .ForMember(dest => dest.Store, opt => opt.Ignore());

                profile.CreateMap<SellerUser, SellerProfileDto>()
                    .ForMember(dest => dest.HasCompany, opt => opt.MapFrom(src => src.CompanyId.HasValue))
                    .ForMember(dest => dest.HasStore, opt => opt.MapFrom(src => src.StoreId.HasValue));

            }
        }
        #endregion

        #region 2️⃣ COMPANY Mappings
        private static class CompanyMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Company, CompanyDto>()
                    .ReverseMap()
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                profile.CreateMap<CompanyCreateDto, Company>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxDocument, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Stores, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerAccount, opt => opt.Ignore());

                profile.CreateMap<CompanyUpdateDto, Company>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TaxDocument, opt => opt.Ignore()) 
                    .ForMember(dest => dest.CompanyNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                profile.CreateMap<CompanyStatusDto, Company>()
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxOffice, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxDocument, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.City, opt => opt.Ignore())
                    .ForMember(dest => dest.Address, opt => opt.Ignore())
                    .ForMember(dest => dest.Email, opt => opt.Ignore())
                    .ForMember(dest => dest.Phone, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Industry, opt => opt.Ignore())
                    .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                profile.CreateMap<CompanyVerifyDto, Company>()
                    .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxOffice, opt => opt.Ignore())
                    .ForMember(dest => dest.TaxDocument, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.City, opt => opt.Ignore())
                    .ForMember(dest => dest.Address, opt => opt.Ignore())
                    .ForMember(dest => dest.Email, opt => opt.Ignore())
                    .ForMember(dest => dest.Phone, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Industry, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerAccount, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());
            }
        }
        #endregion

        #region 3️⃣ CATEGORY Mappings
        private static class CategoryMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Category, CategoryDto>().ReverseMap();

                profile.CreateMap<CategoryCreateDto, Category>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoriesSubs, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<CategoryUpdateDto, Category>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryImage, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoriesSubs, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<CategorySub, CategorySubDto>().ReverseMap();

                profile.CreateMap<CategorySubCreateDto, CategorySub>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.MainCategory, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<CategorySubUpdateDto, CategorySub>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.MainCategoryId, opt => opt.Ignore())
                    .ForMember(dest => dest.MainCategory, opt => opt.Ignore())
                    .ReverseMap();
            }
        }
        #endregion

        #region 5️⃣ STORE Mappings
        private static class StoreMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Store, StoreDto>().ReverseMap();

                profile.CreateMap<StoreCreateDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreCarriers, opt => opt.Ignore())
                    .ForMember(dest => dest.Orders, opt => opt.Ignore())
                    .ForMember(dest => dest.LocationCoverage, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<StoreUpdateDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerId, opt => opt.Ignore())
                    .ForMember(dest => dest.SellerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreCarriers, opt => opt.Ignore())
                    .ForMember(dest => dest.Orders, opt => opt.Ignore())
                    .ForMember(dest => dest.LocationCoverage, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<StoreStatusDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForAllMembers(opt => opt.Ignore());

                profile.CreateMap<StoreProduct, StoreProductListDto>()
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                     .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                     .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                     .ForMember(dest => dest.StoreProductImageUrl, opt => opt.MapFrom(src => src.StoreProductImageUrl));

                profile.CreateMap<StoreProduct, StoreProductDto>()
                     .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                     .ForMember(dest => dest.ProductNumber, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductNumber : null));

            }
        }
        #endregion

        #region 6️⃣ PRODUCT Mappings
        private static class ProductMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<ProductCreateDto, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UnitType, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Category, opt => opt.Ignore()) 
                    .ForMember(dest => dest.CategorySub, opt => opt.Ignore()) 
                    .ForMember(dest => dest.CategoryName, opt => opt.Ignore()) 
                    .ForMember(dest => dest.CategorySubName, opt => opt.Ignore()); 

                profile.CreateMap<ProductUpdateDto, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UnitType, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySub, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySubName, opt => opt.Ignore());

                profile.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.UnitTypes, opt => opt.MapFrom(src => src.UnitTypes));
            }
        }
        #endregion

        #region DeliveryAddress  Mappings

        private static class DeliveryAddressMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<DeliveryAddress, DeliveryAddressDto>()
                    .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                    .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                    .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.Name))
                    .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
                    .ForMember(dest => dest.NeighborhoodName, opt => opt.MapFrom(src => src.Neighborhood.Name));

                profile.CreateMap<DeliveryAddressCreateDto, DeliveryAddress>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhood, opt => opt.Ignore());

                profile.CreateMap<DeliveryAddressUpdateDto, DeliveryAddress>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhood, opt => opt.Ignore());
            }
        }
        #endregion

        #region Locations  Mappings
        private static class LocationMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Country, CountryDto>().ReverseMap();
                profile.CreateMap<CountryCreateDto, Country>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Provinces, opt => opt.Ignore())
                    .ForMember(dest => dest.States, opt => opt.Ignore());

                profile.CreateMap<State, StateDto>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
                profile.CreateMap<StateCreateDto, State>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.Provinces, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

                profile.CreateMap<Province, ProvinceDto>().ReverseMap();
                profile.CreateMap<ProvinceCreateDto, Province>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Districts, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

                profile.CreateMap<District, DistrictDto>().ReverseMap();
                profile.CreateMap<DistrictCreateDto, District>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhoods, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForSourceMember(src => src.ProvinceName, opt => opt.DoNotValidate());

                profile.CreateMap<Neighborhood, NeighborhoodDto>().ReverseMap();
                profile.CreateMap<NeighborhoodCreateDto, Neighborhood>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForSourceMember(src => src.DistrictName, opt => opt.DoNotValidate());
            }
        }
        #endregion

        #region STOREMARKET COVERAGE Mappings
        private static class MarketMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<StoreMarketCountryCreateDto, StoreLocationCountry>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketCountryUpdateDto, StoreLocationCountry>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryId, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationCountry, StoreMarketCountryDto>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));

                profile.CreateMap<StoreMarketProvinceCreateDto, StoreLocationProvince>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketProvinceUpdateDto, StoreLocationProvince>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceId, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationProvince, StoreMarketProvinceDto>()
                    .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province != null ? src.Province.Name : null));

                profile.CreateMap<StoreMarketDistrictCreateDto, StoreLocationDistrict>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketDistrictUpdateDto, StoreLocationDistrict>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictId, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationDistrict, StoreMarketDistrictDto>()
                    .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District != null ? src.District.Name : null));

                profile.CreateMap<StoreMarketNeighborhoodCreateDto, StoreLocationNeighborhood>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhood, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketNeighborhoodUpdateDto, StoreLocationNeighborhood>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodId, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhood, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationNeighborhood, StoreMarketNeighborhoodDto>()
                    .ForMember(dest => dest.NeighborhoodName, opt => opt.MapFrom(src => src.Neighborhood != null ? src.Neighborhood.Name : null));

                profile.CreateMap<StoreMarketRegionCreateDto, StoreLocationRegion>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketRegionUpdateDto, StoreLocationRegion>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionId, opt => opt.Ignore())
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationRegion, StoreMarketRegionDto>()
                    .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region != null ? src.Region.Name : null));

                profile.CreateMap<StoreMarketStateCreateDto, StoreLocationState>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore())
                    .ForMember(dest => dest.StateName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketStateUpdateDto, StoreLocationState>()
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.StateId, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.StateName, opt => opt.Ignore());

                profile.CreateMap<StoreLocationState, StoreMarketStateDto>()
                    .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State != null ? src.State.Name : null));

                profile.CreateMap<StoreMarketCountryMultiCreateDto, StoreLocationCountry>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketProvinceMultiCreateDto, StoreLocationProvince>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketDistrictMultiCreateDto, StoreLocationDistrict>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketNeighborhoodMultiCreateDto, StoreLocationNeighborhood>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhood, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketRegionMultiCreateDto, StoreLocationRegion>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

                profile.CreateMap<StoreMarketStateMultiCreateDto, StoreLocationState>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.StateId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore())
                    .ForMember(dest => dest.StateName, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));
            }
        }
        #endregion

        #region StoreProductRequestMappings Mappings

        private static class StoreProductRequestMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<StoreProductRequest, StoreProductRequestDto>();

                profile.CreateMap<StoreProductRequest, StoreProductRequestDetailDto>();

                profile.CreateMap<StoreProductRequestCreateDto, StoreProductRequest>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) 
                    .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(_ => false))
                    .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySubName, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySub, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => StoreProductRequestStatus.Pending))
                    .ForMember(dest => dest.Specifications, opt => opt.Ignore())
                    .ForMember(dest => dest.ReviewedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.ApprovedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.AdminNote, opt => opt.Ignore());

                profile.CreateMap<StoreProductRequestUpdateDto, StoreProductRequest>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                    .ForMember(dest => dest.Store, opt => opt.Ignore())
                    .ForMember(dest => dest.UnitType, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Specifications, opt => opt.Ignore())
                    .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySubId, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySub, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySubName, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.AdminNote, opt => opt.Ignore())
                    .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.ReviewedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.ApprovedAt, opt => opt.Ignore());

                profile.CreateMap<StoreProductRequest, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ProductNumber, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Barcode, opt => opt.Ignore()) 
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.PreparationTime, opt => opt.Ignore())
                    .ForMember(dest => dest.ExpirationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.Category, opt => opt.Ignore())
                    .ForMember(dest => dest.CategorySub, opt => opt.Ignore())
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                    .ForMember(dest => dest.CategorySubName, opt => opt.MapFrom(src => src.CategorySubName));
            }
        }
        #endregion

        #region BASKET Mappings
        private static class BasketMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Basket, BasketDto>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ReverseMap()
                    .ForMember(dest => dest.BuyerId, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

                profile.CreateMap<BasketItem, BasketItemDto>().ReverseMap()
                    .ForMember(dest => dest.StoreProductImageUrl, opt => opt.MapFrom(src => src.StoreProductImageUrl));

                profile.CreateMap<BasketAddToDto, BasketItem>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.BasketId, opt => opt.Ignore())
                        .ForMember(dest => dest.StoreId, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Basket, opt => opt.Ignore())
                    .ForMember(dest => dest.ProductName, opt => opt.Ignore())
                    .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
                    .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreProductImageUrl, opt => opt.Ignore()); 
            }
        }
        #endregion


        #region Payment Mappings

        private static class PaymentMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Payment, PaymentDto>();

                profile.CreateMap<PaymentCreateDto, Payment>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerId, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.PaymentReference, opt => opt.Ignore())
                    .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
                    .ForMember(dest => dest.ErrorCode, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.PaidAmount, opt => opt.Ignore())
                    .ForMember(dest => dest.PaidPrice, opt => opt.Ignore())
                    .ForMember(dest => dest.OrderNumber, opt => opt.Ignore());

            }
        }
        #endregion

        #region Availability Mappings

        private static class AvailabilityMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Store, AvailableStoreDto>()
                    .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreName))
                    .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                    .ForMember(dest => dest.DeliveryTimeFrame, opt => opt.Ignore())
                    .ForMember(dest => dest.RegionId, opt => opt.Ignore())
                    .ForMember(dest => dest.CountryId, opt => opt.Ignore())
                    .ForMember(dest => dest.StateId, opt => opt.Ignore())
                    .ForMember(dest => dest.ProvinceId, opt => opt.Ignore())
                    .ForMember(dest => dest.DistrictId, opt => opt.Ignore())
                    .ForMember(dest => dest.NeighborhoodId, opt => opt.Ignore());

                profile.CreateMap<Store, AvailableStoreWithProductsDto>()
                    .IncludeBase<Store, AvailableStoreDto>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore()); 

            }
        }
        #endregion

        #region Carrier  Mappings
        private static class CarrierMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Carrier, CarrierDto>()
                    .ForMember(dest => dest.IntegrationType, opt => opt.MapFrom(src => src.IntegrationType.ToString()));

                profile.CreateMap<CarrierCreateDto, Carrier>();
            }
        }
        #endregion

        #region StoreCarrier  Mappings
        private static class StoreCarrierMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<StoreCarrier, StoreCarrierDto>()
                    .ForMember(dest => dest.CarrierName, opt => opt.MapFrom(src => src.Carrier.Name))
                    .ForMember(dest => dest.CarrierLogoUrl, opt => opt.MapFrom(src => src.Carrier.CarrierLogoUrl));

                profile.CreateMap<StoreCarrierCreateDto, StoreCarrier>()
                    .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => true)); 
            }
        }
        #endregion
    }
}
