using AutoMapper;
using Data.Dtos.Auths;
using Data.Dtos.Companys;
using Entity.Auths;
using Entity.Companies;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AuthMappings.RegisterMappings(this);
            CompanyMappings.RegisterMappings(this);
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
                    .ForMember(dest => dest.Store, opt => opt.Ignore());
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

    }
}
