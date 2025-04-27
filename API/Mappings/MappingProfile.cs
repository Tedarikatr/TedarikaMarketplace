using AutoMapper;
using Data.Dtos.Auths;
using Data.Dtos.Categories;
using Data.Dtos.Companies;
using Data.Dtos.DeliveryAddresses;
using Data.Dtos.Markets;
using Data.Dtos.Product;
using Data.Dtos.Stores;
using Entity.Auths;
using Entity.Categories;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Markets;
using Entity.Markets.Locations;
using Entity.Products;
using Entity.Stores;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AuthMappings.RegisterMappings(this);
            CompanyMappings.RegisterMappings(this);
            CategoryMappings.RegisterMappings(this);
            MarketMappings.RegisterMappings(this);
            StoreMappings.RegisterMappings(this);
            ProductMappings.RegisterMappings(this);
            DeliveryAddressMappings.RegisterMappings(this);
            LocationMappings.RegisterMappings(this);
        }

        #region 1️⃣ AUTH Mappings
        private static class AuthMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                // BuyerUser <-> BuyerUserDto
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

                // SellerUser <-> SellerUserDto
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
                // Company <-> CompanyDto
                profile.CreateMap<Company, CompanyDto>()
                    .ReverseMap()
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                // CompanyCreateDto -> Company
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

                // CompanyUpdateDto -> Company
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

                // Şirketin aktiflik durumu değiştirme (Admin)
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

                // Şirketin onay durumu değiştirme (Admin)
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

        #region 4️⃣ MARKET Mappings
        private static class MarketMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<Market, MarketDto>().ReverseMap();

                profile.CreateMap<MarketCreateDto, Market>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.DeliveryTimeFrame, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreMarkets, opt => opt.Ignore())
                    .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.MarketType))
                    .ReverseMap();

                profile.CreateMap<MarketUpdateDto, Market>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.DeliveryTimeFrame, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreMarkets, opt => opt.Ignore())
                    .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.MarketType))
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
                    .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                    .ForMember(dest => dest.Owner, opt => opt.Ignore())
                    .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreMarkets, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreCarriers, opt => opt.Ignore())
                    .ForMember(dest => dest.Orders, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<StoreUpdateDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                    .ForMember(dest => dest.Owner, opt => opt.Ignore())
                    .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                    .ForMember(dest => dest.Company, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreMarkets, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreProducts, opt => opt.Ignore())
                    .ForMember(dest => dest.StoreCarriers, opt => opt.Ignore())
                    .ForMember(dest => dest.Orders, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<StoreStatusDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForAllMembers(opt => opt.Ignore());

                profile.CreateMap<StorePaymentMethodDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForAllMembers(opt => opt.Ignore());

                profile.CreateMap<StoreDeliveryOptionDto, Store>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForAllMembers(opt => opt.Ignore());
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

        #region 6️⃣ DeliveryAddress  Mappings

        private static class DeliveryAddressMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                profile.CreateMap<DeliveryAddress, DeliveryAddressDto>()
                    .ReverseMap();

                profile.CreateMap<DeliveryAddressCreateDto, DeliveryAddress>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore()) // ❗ Ignore navigation
                    .ForMember(dest => dest.BuyerUserId, opt => opt.MapFrom(src => src.BuyerUserId))
                    .ReverseMap();

                profile.CreateMap<DeliveryAddressUpdateDto, DeliveryAddress>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.BuyerUserId, opt => opt.Ignore()) // ❗ Güncelleme DTO'sunda user ID gelmiyor
                    .ForMember(dest => dest.BuyerUser, opt => opt.Ignore())  // ❗ Navigation
                    .ReverseMap();
            }
        }

        #endregion

        #region 6️⃣ Locations  Mappings
        private static class LocationMappings
        {
            public static void RegisterMappings(Profile profile)
            {
                // Country
                profile.CreateMap<Country, CountryDto>().ReverseMap();
                profile.CreateMap<CountryCreateDto, Country>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Provinces, opt => opt.Ignore())
                    .ForMember(dest => dest.States, opt => opt.Ignore()); // ✔️ HATA BURADAN GELİYORDU

                // State
                profile.CreateMap<State, StateDto>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
                profile.CreateMap<StateCreateDto, State>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // ✔️ Id atlanmalı
                    .ForMember(dest => dest.Country, opt => opt.Ignore()) // ✔️ Navigation property
                    .ForMember(dest => dest.Provinces, opt => opt.Ignore()) // ✔️ ICollection ilişkisi
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)); // ✔️ Varsayılan true

                // Province
                profile.CreateMap<Province, ProvinceDto>().ReverseMap();
                profile.CreateMap<ProvinceCreateDto, Province>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore())
                    .ForMember(dest => dest.State, opt => opt.Ignore()) // ✔️ Eklenen navigation
                    .ForMember(dest => dest.Districts, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

                // District
                profile.CreateMap<District, DistrictDto>().ReverseMap();
                profile.CreateMap<DistrictCreateDto, District>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Province, opt => opt.Ignore())
                    .ForMember(dest => dest.Neighborhoods, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForSourceMember(src => src.ProvinceName, opt => opt.DoNotValidate());

                // Neighborhood
                profile.CreateMap<Neighborhood, NeighborhoodDto>().ReverseMap();
                profile.CreateMap<NeighborhoodCreateDto, Neighborhood>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.District, opt => opt.Ignore())
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForSourceMember(src => src.DistrictName, opt => opt.DoNotValidate());
            }
        }


        #endregion
    }
}
