using AutoMapper;
using Data.Dto.Auth;
using Entity.Auth;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AuthMappings.RegisterMappings(this);
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
                    .ForMember(dest => dest.GuidNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ReverseMap();

                profile.CreateMap<SellerLoginDto, SellerUser>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
            }
        }
        #endregion
    }
}
