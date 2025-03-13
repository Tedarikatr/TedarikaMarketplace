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
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.UserNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<BuyerUser, BuyerUserInfoDto>()
                    .ReverseMap();

                profile.CreateMap<BuyerLoginDto, BuyerUser>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());

                // SellerUser <-> SellerUserDto
                profile.CreateMap<SellerUser, SellerUserDto>().ReverseMap();

                profile.CreateMap<SellerRegisterDto, SellerUser>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserGuidNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<SellerLoginDto, SellerUser>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
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
                    .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.CompanyUsers.Count))
                    .ReverseMap()
                    .ForMember(dest => dest.CompanyUsers, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                // CompanyCreateDto -> Company
                profile.CreateMap<CompanyCreateDto, Company>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.IsVerified, opt => opt.Ignore()) 
                    .ForMember(dest => dest.IsActive, opt => opt.Ignore())   
                    .ForMember(dest => dest.CompanyUsers, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                // CompanyUpdateDto -> Company
                profile.CreateMap<CompanyUpdateDto, Company>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CompanyUsers, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                // Şirketin aktiflik durumu değiştirme (Admin)
                profile.CreateMap<CompanyStatusDto, Company>()
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                    .ForMember(dest => dest.CompanyUsers, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());

                // Şirketin onay durumu değiştirme (Admin)
                profile.CreateMap<CompanyVerifyDto, Company>()
                    .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
                    .ForMember(dest => dest.CompanyUsers, opt => opt.Ignore())
                    .ForMember(dest => dest.Stores, opt => opt.Ignore());
            }
        }
        #endregion
    }
}
