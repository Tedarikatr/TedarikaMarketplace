using FluentValidation;
using Data.Dtos.Auths;

namespace API.Validators.Auth
{
    public class AuthValidator
    {
        public class BuyerUserCreateValidator : AbstractValidator<BuyerUserCreateDto>
        {
            public BuyerUserCreateValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş olamaz.");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş olamaz.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
                RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası gereklidir.");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre gereklidir.")
                    .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            }
        }

        public class BuyerLoginValidator : AbstractValidator<BuyerLoginDto>
        {
            public BuyerLoginValidator()
            {
                RuleFor(x => x.EmailOrPhone).NotEmpty().WithMessage("E-posta veya telefon alanı boş olamaz.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            }
        }

        public class SellerRegisterValidator : AbstractValidator<SellerRegisterDto>
        {
            public SellerRegisterValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş olamaz.");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş olamaz.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
                RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası gereklidir.");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre gereklidir.")
                    .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            }
        }

        public class SellerLoginValidator : AbstractValidator<SellerLoginDto>
        {
            public SellerLoginValidator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            }
        }
    }
}
